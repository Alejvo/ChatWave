using Application.Friends.AddFriend;
using Application.Friends.GetRequests;
using Application.Friends.MakeRequest;
using Application.Friends.RemoveFriend;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/friends")]
[Authorize]
public class FriendsController : ApiController
{
    private readonly ISender _sender;

    public FriendsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    [Route("add")]
    public async Task<IActionResult> AddFriend([FromBody] AddFriendCommand command)
    {
       var res = await _sender.Send(command);
        return res.IsSuccess ? Ok() : Problem(res.Errors);
    }

    [HttpPost]
    [Route("remove")]
    public async Task<IActionResult> RemoveFriend([FromBody] RemoveFriendCommand command)
    {
        var res = await _sender.Send(command);
        return res.IsSuccess ? Ok() : Problem(res.Errors);
    }

    [HttpPost]
    [Route("request")]
    public async Task<IActionResult> MakeFriendRequest([FromBody] MakeFriendRequestCommand command)
    {
        var res = await _sender.Send(command);
        return res.IsSuccess ? Ok() : Problem(res.Errors);
    }

    [HttpGet]
    [Route("request")]
    public async Task<IActionResult> GetRequests([FromQuery] GetFriendRequestsQuery command)
    {
        var res = await _sender.Send(command);
        return res.IsSuccess ? Ok(res) : Problem(res.Errors);
    }
}
