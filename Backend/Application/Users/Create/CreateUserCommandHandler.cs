using Application.Abstractions;
using Domain.Users;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Create
{
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
            var user = User.Create(
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
}
