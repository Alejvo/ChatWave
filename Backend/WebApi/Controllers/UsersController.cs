using Application.Users.Create;
using Application.Users.Delete;
using Application.Users.Get.All;
using Application.Users.Get.Id;
using Application.Users.Get.Username;
using Application.Users.Update;
using Domain.Users;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace WebApi.Controllers
{
    [Route("api/users")]
    public class UsersController : ApiController
    {
        private readonly ISender _sender;
        private readonly IUserRepository _userRepository;
        
        public UsersController(ISender sender,IUserRepository userRepository)
        {
            _sender = sender;
            _userRepository = userRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateUserCommand command)
        {
            Result res = await _sender.Send(command);
            return res.IsSuccess ? Created() : Problem(res.Errors);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var command = new GetUsersQuery();
            var res = await _sender.Send(command);
            return res.IsSuccess ? Ok(res) : Problem(res.Errors);
        }

        [HttpGet("id/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var command = new GetUserByIdQuery(id);
            var res = await _sender.Send(command);
            return res.IsSuccess ? Ok(res) : Problem(res.Errors);
        }

        [HttpGet("username/{username}")]
        public async Task<IActionResult> GetByUsername(string username)
        {
            var command = new GetUsersByUsernameQuery(username);
            var res = await _sender.Send(command);
            return res.IsSuccess ? Ok(res) : Problem(res.Errors);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromForm] UpdateUserCommand command)
        {
            var res = await _sender.Send(command);
            return res.IsSuccess ? NoContent() : Problem(res.Errors);

        }

        [HttpDelete("id/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var command = new DeleteUserCommand(id);
            var res = await _sender.Send(command);
            return res.IsSuccess ? NoContent() : Problem(res.Errors);
        }
    }
}
