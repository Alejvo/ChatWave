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
        var destUser = await _userRepository.GetById(request.DestinyId);
        if (destUser == null) return Result.Failure<IEnumerable<MessageResponse>>(UserErrors.NotFound(request.DestinyId));

        var originUser = await _userRepository.GetById(request.OriginId);
        if (originUser == null) return Result.Failure<IEnumerable<MessageResponse>>(UserErrors.NotFound(request.OriginId));

        var userMessage = await _messageRepository.GetUserMessages(request.OriginId, request.DestinyId);
        var messages = userMessage.Select(message => message.ToMessageResponse(request.OriginId));
        return Result.Success(messages);
    }
}
