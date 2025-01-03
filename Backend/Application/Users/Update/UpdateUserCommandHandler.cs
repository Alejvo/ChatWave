using Application.Abstractions;
using Domain.Users;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Update
{
    internal sealed class UpdateUserCommandHandler : ICommandHandler<UpdateUserCommand>
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            byte[] profileImageBytes = null;
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

            await _userRepository.UpdateAsync(new
            {
                user.Id,
                request.FirstName,
                request.LastName,
                request.Email,
                request.Password,
                request.Birthday,
                request.Username,
                ProfileImage = profileImageBytes
            });
            return Result.Success();
        }
    }
}