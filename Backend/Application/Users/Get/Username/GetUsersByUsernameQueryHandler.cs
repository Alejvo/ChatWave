using Application.Abstractions;
using Application.Users.Common;
using Domain.Users;

namespace Application.Users.Get.Username;

public sealed class GetUsersByUsernameQueryHandler : IQueryHandler<GetUsersByUsernameQuery, IEnumerable<UserResponse>>
{
    private readonly IUserRepository _userRepository;

    public GetUsersByUsernameQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<IEnumerable<UserResponse>>> Handle(GetUsersByUsernameQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUsersByUsername(request.username);
        if (user == null) UserErrors.NotFoundByUsername(request.username);
   
        var userResponse = user.Select(user => UserResponse.ToUserResponse(user));
        return userResponse.ToList();
    }
}
