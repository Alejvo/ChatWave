using Application.Abstractions;
using Application.Users.Common;
using Domain.Users;

namespace Application.Users.Get.All
{
    internal sealed class GetUsersQueryHandler : IQueryHandler<GetUsersQuery, IEnumerable<UserResponse>>
    {
        private readonly IUserRepository _userRepository;

        public GetUsersQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<IEnumerable<UserResponse>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAll();
            var usersResponse = users.Select(user => UserResponse.ToUserResponse(user)).ToList();
            return usersResponse;
        }
    }
}
