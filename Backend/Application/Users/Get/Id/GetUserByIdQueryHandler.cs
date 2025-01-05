using Application.Abstractions;
using Application.Users.Common;
using Domain.Users;
using Shared;

namespace Application.Users.Get.Id;

internal sealed class GetUserByIdQueryHandler : IQueryHandler<GetUserByIdQuery, UserResponse>
{
    private readonly IUserRepository _userRepository;

    public GetUserByIdQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<UserResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetById(request.Id);
        if (user == null) return Result.Failure<UserResponse>(UserErrors.NotFound(request.Id)); 

        var userResponse = user.ToUserResponse();
        return userResponse;
    }
}
