using Application.Abstractions;
using Domain.Users;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Delete
{
    internal sealed class DeleteUserCommandHandler : ICommandHandler<DeleteUserCommand>
    {
        private readonly IUserRepository _userRepository;

        public DeleteUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetById(request.Id);

            if (user == null) return UserErrors.NotFound(request.Id);

            await _userRepository.DeleteAsync(user.Id);
            return Result.Success();
        }
    }
}
