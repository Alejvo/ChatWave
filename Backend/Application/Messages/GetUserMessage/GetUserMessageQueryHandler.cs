using Application.Abstractions;
using Application.Messages.Common;
using Domain.Groups;
using Domain.Messages;
using Domain.Users;
using Shared;

namespace Application.Messages.GetUserMessage;

internal sealed class GetUserMessageQueryHandler : IQueryHandler<GetUserMessageQuery, IEnumerable<MessageResponse>>
{
    private readonly IMessageRepository _messageRepository;
    private readonly IUserRepository _userRepository;

    public GetUserMessageQueryHandler(IMessageRepository messageRepository, IUserRepository userRepository)
    {
        _messageRepository = messageRepository;
        _userRepository = userRepository;
    }

    public async Task<Result<IEnumerable<MessageResponse>>> Handle(GetUserMessageQuery request, CancellationToken cancellationToken)
    {
        var receiver = await _userRepository.GetById(request.ReceiverId);
        if (receiver == null) return Result.Failure<IEnumerable<MessageResponse>>(UserErrors.NotFound(request.ReceiverId));

        var sender = await _userRepository.GetById(request.SenderId);
        if (sender == null) return Result.Failure<IEnumerable<MessageResponse>>(UserErrors.NotFound(request.SenderId));

        var userMessage = await _messageRepository.GetUserMessages(request.ReceiverId,request.SenderId);
        var messages = userMessage.Select(message => message.ToMessageResponse(request.SenderId));
        return Result.Success(messages);
    }
}
