using Application.Messages.GetGroupMessage;
using Application.Messages.GetUserMessage;
using Application.Messages.SendGroupMessage;
using Application.Messages.SendUserMessage;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Application.Hubs;

[Authorize]
public class ChatHub : Hub 
{
    private readonly ISender _sender;

    public ChatHub(ISender sender)
    {
        _sender = sender;
    }
    /*
    public override Task OnConnectedAsync()
    {
        Console.WriteLine($"ConnId:{Context.ConnectionId}, User:{Context.UserIdentifier}");
        return base.OnConnectedAsync();
    }*/

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
}
