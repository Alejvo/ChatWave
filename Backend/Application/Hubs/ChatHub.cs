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

    public override Task OnConnectedAsync()
    {
        Console.WriteLine($"ConnId:{Context.ConnectionId}, User:{Context.UserIdentifier}");
        return base.OnConnectedAsync();
    }

    public async Task SendMessageToUser(string receiver, string sender, string message)
    {
        var sentAt = DateTime.UtcNow;
        var newMessage = await _sender.Send(new SendUserMessageCommand(message, sender, receiver, sentAt));
        if(newMessage.IsSuccess)
        {
            await Clients.User(receiver).SendAsync("ReceiveMessage", newMessage);
            await Clients.User(sender).SendAsync("ReceiveMessage", newMessage);
        }
    }
    public async Task GetUserMessages(string receiver, string sender)
    {
        var messages = await _sender.Send(new GetUserMessageQuery(receiver, sender));
        await Clients.Caller.SendAsync("ReceiveMessageHistory", messages.Value);
    }

    public async Task GetGroupMessages(string group, string user)
    {
        var messages = await _sender.Send(new GetGroupMessageQuery(user, group));
        await Groups.AddToGroupAsync(Context.ConnectionId, group);
        await Clients.Caller.SendAsync("ReceiveMessageHistory", messages.Value);
    }
    public async Task SendMessageToGroup(string group, string sender, string message)
    {
        var newMessage = await _sender.Send(new SendGroupMessageCommand(message, sender, group, DateTime.Now));
        await Clients.Group(group).SendAsync("ReceiveMessage", newMessage);
    }
}
