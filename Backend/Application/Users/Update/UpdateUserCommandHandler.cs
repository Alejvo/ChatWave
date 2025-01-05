using Application.Abstractions;
using Domain.Users;
using Shared;

namespace Application.Users.Update;

internal sealed class UpdateUserCommandHandler : ICommandHandler<UpdateUserCommand>
{
    private readonly IUserRepository _userRepository;

    public UpdateUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
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

        await _userRepository.UpdateAsync(updatedUser);
        return Result.Success();
    }
}