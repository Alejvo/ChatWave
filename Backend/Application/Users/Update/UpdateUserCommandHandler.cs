using Application.Abstractions;
using Domain.Users;
using Domain.Users.Events;
using Shared;

namespace Application.Users.Update;

internal sealed class UpdateUserCommandHandler : ICommandHandler<UpdateUserCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IEventStore _eventStore;

    public UpdateUserCommandHandler(IUserRepository userRepository, IEventStore eventStore)
    {
        _userRepository = userRepository;
        _eventStore = eventStore;
    }

    public async Task<Result> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        byte[]? profileImageBytes = null;
        var user = await _userRepository.GetById( request.Id );
        if (user == null) return UserErrors.NotFound(request.Id);
        if (request.ProfileImage != null)
        {
            using (var memoryStream = new MemoryStream())
            {
                await request.ProfileImage.CopyToAsync(memoryStream);
                profileImageBytes = memoryStream.ToArray();
            }
        }

        var updatedUser = UserRequest.Create(
            request.Id,
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password,
            request.Birthday,
            request.Username,
           profileImageBytes
            );

        var userUpdated = new UserUpdatedEvent(updatedUser.Id,$"{updatedUser.FirstName} {updatedUser.LastName}",updatedUser.Email,updatedUser.Username);

        await _userRepository.UpdateAsync(updatedUser);
        await _eventStore.SaveEventAsync(userUpdated, EntityType.User);
        return Result.Success();
    }
}