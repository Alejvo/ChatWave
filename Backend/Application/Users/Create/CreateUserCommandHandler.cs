using Application.Abstractions;
using Domain.Users;
using MediatR;
using Shared;

namespace Application.Users.Create;

internal sealed class CreateUserCommandHandler : ICommandHandler<CreateUserCommand>
{
    private readonly IUserRepository _userRepository;

    public CreateUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
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
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password,
            request.Birthday,
            request.UserName,
            profileImageBytes);

        await _userRepository.CreateAsync(user);
        return Result.Success();
    }
}
