using Application.Abstractions;
using Domain.Users;
using Domain.Users.Events;
using MediatR;
using Shared;

namespace Application.Users.Create;

internal sealed class CreateUserCommandHandler : ICommandHandler<CreateUserCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IEventStore _eventStore;

    public CreateUserCommandHandler(IUserRepository userRepository, IEventStore eventStore)
    {
        _userRepository = userRepository;
        _eventStore = eventStore;
    }

    public async Task<Result> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        byte[] profileImageBytes = null;
        if (request.ProfileImage != null)
        {
            using (var memoryStream = new MemoryStream())
            {
                await request.ProfileImage.CopyToAsync(memoryStream);
                profileImageBytes = memoryStream.ToArray();
            }
        }


        var user = UserRequest.Create(
            Guid.NewGuid().ToString(),
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password,
            request.Birthday,
            request.UserName,
            profileImageBytes);
        
        var userCreated = new UserCreatedEvent(user.Id,$"{user.FirstName} {user.LastName}",user.Email,user.Username);

        await _userRepository.CreateAsync(user);
        await _eventStore.SaveEventAsync(userCreated,EntityType.User);

        return Result.Success();
    }
}
