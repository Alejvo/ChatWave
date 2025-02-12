using Application.Abstractions;
using Application.Users.Create;
using Application.Users.Delete;
using Application.Users.GetAll;
using Application.Users.GetById;
using Application.Users.Update;
using Domain.Users;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace WebApi.Controllers
{
    [Route("api/users")]
    [Authorize]
    public class UsersController : ApiController
    {
        private readonly ISender _sender;
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;

        public UsersController(ISender sender, IUserRepository userRepository, IAuthService authService)
        {
            _sender = sender;
            _userRepository = userRepository;
            _authService = authService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Create([FromForm] CreateUserCommand command)
        {
            Result res = await _sender.Send(command);
            return res.IsSuccess ? Created() : Problem(res.Errors);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
            string? searchTerm,
            string? sortColumn,
            string? sortOrder,
            int page,
            int pageSize,
            string currentUserId)
        {
            var command = new GetUsersQuery(searchTerm,sortColumn,sortOrder,page,pageSize,currentUserId);
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

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _userRepository.LoginUser(request.Email,request.Password);
            if(user == null) return BadRequest();

            var token = _authService.GenerateToken(user.Id, user.Username);
            var refreshToken = _authService.GenerateRefreshToken();

            await _authService.SaveRefreshToken(user.Id, refreshToken);
            return Ok(new { token, refreshToken });
        }

    }
}
