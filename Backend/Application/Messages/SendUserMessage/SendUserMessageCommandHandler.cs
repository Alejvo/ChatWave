using Application.Abstractions;
using Domain.Groups;
using Domain.Messages;
using Domain.Users;
using Shared;

namespace Application.Messages.SendUserMessage;

internal sealed class SendUserMessageCommandHandler : ICommandHandler<SendUserMessageCommand>
{
    private readonly IMessageRepository _messageRepository;
    private readonly IUserRepository _userRepository;

    public SendUserMessageCommandHandler(IMessageRepository messageRepository, IUserRepository userRepository)
    {
        _messageRepository = messageRepository;
        _userRepository = userRepository;
    }

    public async Task<Result> Handle(SendUserMessageCommand request, CancellationToken cancellationToken)
    {
        var  receiver = await _userRepository.GetById(request.SenderId);
        if (receiver == null) return Result.Failure<IEnumerable<GroupMessage>>(UserErrors.NotFound(request.ReceiverId));

        var sender = await _userRepository.GetById(request.SenderId);
        if (sender == null) return Result.Failure<IEnumerable<GroupMessage>>(UserErrors.NotFound(request.SenderId));

        var message = new UserMessage(
            Guid.NewGuid().ToString(),
            request.Text,
            request.SenderId,
            request.ReceiverId,
            request.SentAt
            );

        await _messageRepository.SendToUser(message);
        return Result.Success();
    }
}
