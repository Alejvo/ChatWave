using Application.Abstractions;
using Application.Users.Common;
using Domain.Users;
using Shared;

namespace Application.Users.GetAll
{
    internal sealed class GetUsersQueryHandler : IQueryHandler<GetUsersQuery, PagedList<UserResponse>>
    {
        private readonly IUserRepository _userRepository;

        public GetUsersQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<PagedList<UserResponse>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var usersQuery = await _userRepository.GetAll();
            var userResponseQuery = usersQuery
                .Where(user => user.Id != request.currentUserId)
                .Select(user => user.ToUserResponse());

            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                userResponseQuery = userResponseQuery.Where(u =>
                    u.Username.StartsWith(request.SearchTerm, StringComparison.OrdinalIgnoreCase) ||
                    u.FullName.StartsWith(request.SearchTerm, StringComparison.OrdinalIgnoreCase));
            }

            Func<UserResponse, object> keySelector = request.SortColumn?.ToLower() switch
            {
                "username" => user => user.Username,
                "name" => user => user.FullName,
                _ => user => user.Id
            };

            userResponseQuery = request.SortOrder is not null
                ? userResponseQuery.OrderByDescending(keySelector)
                : userResponseQuery.OrderBy(keySelector);

            userResponseQuery = userResponseQuery.ToList();

            var users = await PagedList<UserResponse>.CreateAsync(
                userResponseQuery,
                request.Page,
                request.PageSize);

            return users;

        }
    }
}
