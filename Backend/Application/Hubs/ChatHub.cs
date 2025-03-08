using Application.Friends.GetByUser;
using Application.Friends.GetRequests;
using Application.Groups.GetAll;
using Application.Groups.GetByUser;
using Application.Messages.GetGroupMessage;
using Application.Messages.GetUserMessage;
using Application.Messages.SendGroupMessage;
using Application.Messages.SendUserMessage;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Application.Hubs;

[Authorize]
public sealed class ChatHub : Hub 
{
    private readonly ISender _sender;

    public ChatHub(ISender sender)
    {
        _sender = sender;
    }

    public async Task SendMessageToUser(string originId, string destinyId, string message)
    {
        var sentAt = DateTime.UtcNow;
        var newMessage = await _sender.Send(new SendUserMessageCommand(message, originId, destinyId, sentAt));
        if(newMessage.IsSuccess)
        {
            await Clients.User(destinyId).SendAsync("ReceiveMessage", newMessage.Value);
            await Clients.User(originId).SendAsync("ReceiveMessage", newMessage.Value);
        }
    }

    public async Task GetUserMessages(string originId, string destinyId)
    {
        var messages = await _sender.Send(new GetUserMessageQuery(originId,destinyId));
        await Clients.Caller.SendAsync("ReceiveMessageHistory", messages.Value);
    }

    public async Task GetGroupMessages(string groupId, string userId)
    {
        var messages = await _sender.Send(new GetGroupMessageQuery(userId, groupId));
        await Groups.AddToGroupAsync(Context.ConnectionId, groupId);
        await Clients.Caller.SendAsync("ReceiveMessageHistory", messages.Value);
    }
    
    public async Task SendMessageToGroup(string groupId, string userId, string message)
    {
        var newMessage = await _sender.Send(new SendGroupMessageCommand(message, userId, groupId, DateTime.Now));
        await Clients.Group(groupId).SendAsync("ReceiveMessage", newMessage.Value);
    }

    public async Task GetFriendList(string userId)
    {
        var friends = await _sender.Send(new GetFriendsByUserQuery(userId));
        await Clients.User(userId).SendAsync("GetFriends",friends.Value);
    }

    public async Task GetGroupList(string userId)
    {
        var groups = await _sender.Send(new GetGroupByUserQuery(userId));
        await Clients.User(userId).SendAsync("GetGroups",groups.Value);
    }


    public async Task GetFriendRequests(string userId)
    {
        var friendRequests = await _sender.Send(new GetFriendRequestsQuery(userId));
        await Clients.User(userId).SendAsync("GetFriendRequests",friendRequests.Value);
    }

    public async Task NotifyFriendRequest(string destinyId, string username)
    {
        await Clients.User(destinyId).SendAsync("NotifyRequest", $"@{username} send you a friend request");
    }
}