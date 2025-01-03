using Application.Users.Create;
using Application.Users.Get.All;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
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
            return res.IsSuccess ? Ok() : BadRequest();
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var command = new GetUsersQuery();
            var res = await _sender.Send(command);
            return res.IsSuccess ? Ok(res) : BadRequest();
        }
    }
}
