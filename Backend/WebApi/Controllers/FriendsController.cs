﻿
using Application.Abstractions;
using Application.Friends.AddFriend;
using Application.Friends.GetRequests;
using Application.Friends.MakeRequest;
using Application.Friends.RemoveFriend;
using Application.Hubs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace WebApi.Controllers;

[Route("api/friends")]
[Authorize]
public class FriendsController : ApiController
{
    private readonly ISender _sender;
    private readonly IUserCacheService _userCache;

    public FriendsController(ISender sender, IUserCacheService userCache)
    {
        _sender = sender;
        _userCache = userCache;
    }

    [HttpPost]
    [Route("add")]
    public async Task<IActionResult> AddFriend([FromBody] AddFriendCommand command)
    {
       var res = await _sender.Send(command);
        return res.IsSuccess ? NoContent() : Problem(res.Errors);
    }

    [HttpPost]
    [Route("remove")]
    public async Task<IActionResult> RemoveFriend([FromBody] RemoveFriendCommand command)
    {
        var res = await _sender.Send(command);
        return res.IsSuccess ? NoContent() : Problem(res.Errors);
    }

    [HttpPost]
    [Route("request")]
    public async Task<IActionResult> MakeFriendRequest([FromBody] MakeFriendRequestCommand command)
    {
        var res = await _sender.Send(command);
        return res.IsSuccess ? NoContent() : Problem(res.Errors);
    }

    [HttpGet]
    [Route("request")]
    public async Task<IActionResult> GetRequests(string userId)
    {
        var res = await _sender.Send(new GetFriendRequestsQuery(userId));
        return res.IsSuccess ? Ok(res) : Problem(res.Errors);
    }
}