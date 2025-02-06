using Application.Abstractions;
using Application.Messages.Common;
using Domain.Groups;
using Domain.Messages;
using Domain.Users;
using Shared;

namespace Application.Messages.SendUserMessage;

internal sealed class SendUserMessageCommandHandler : ICommandHandler<SendUserMessageCommand,MessageResponse>
{
    private readonly IMessageRepository _messageRepository;
    private readonly IUserRepository _userRepository;

    public SendUserMessageCommandHandler(IMessageRepository messageRepository, IUserRepository userRepository)
    {
        _messageRepository = messageRepository;
        _userRepository = userRepository;
    }

    public async Task<Result<MessageResponse>> Handle(SendUserMessageCommand request, CancellationToken cancellationToken)
    {
        var  destUser = await _userRepository.GetById(request.DestinyId);
        if (destUser == null) return Result.Failure<MessageResponse>(UserErrors.NotFound(request.DestinyId));

        var sender = await _userRepository.GetById(request.OriginId);
        if (sender == null) return Result.Failure<MessageResponse>(UserErrors.NotFound(request.OriginId));

        var id = Guid.NewGuid().ToString();
        var message = UserMessage.Create(id,request.Text,request.OriginId,request.DestinyId,request.SentAt);

        await _messageRepository.SendToUser(message);
        return Result.Success(message.ToMessageResponse(sender.Id));
    }
}
