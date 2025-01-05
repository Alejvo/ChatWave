using Application.Users.Create;
using Application.Users.Delete;
using Application.Users.Get.All;
using Application.Users.Get.Id;
using Application.Users.Get.Username;
using Application.Users.Update;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ISender _sender;

        public UsersController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateUserCommand command)
        {
            var res = await _sender.Send(command);
            return res.IsSuccess ? Created() : BadRequest();
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var command = new GetUsersQuery();
            var res = await _sender.Send(command);
            return res.IsSuccess ? Ok(res) : BadRequest();
        }

        [HttpGet("id/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var command = new GetUserByIdQuery(id);
            var res = await _sender.Send(command);
            return res.IsSuccess ? Ok(res) : BadRequest();
        }

        [HttpGet("username/{username}")]
        public async Task<IActionResult> GetByUsername(string username)
        {
            var command = new GetUsersByUsernameQuery(username);
            var res = await _sender.Send(command);
            return res.IsSuccess ? Ok(res) : BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromForm] UpdateUserCommand command)
        {
            var res = await _sender.Send(command);
            return res.IsSuccess ? Ok() : BadRequest();
        }

        [HttpDelete("id/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var command = new DeleteUserCommand(id);
            var res = await _sender.Send(command);
            return res.IsSuccess ? Ok() : BadRequest();
        }
    }
}
