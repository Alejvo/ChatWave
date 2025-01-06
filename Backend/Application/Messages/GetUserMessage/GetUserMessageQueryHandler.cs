using Application.Abstractions;
using Domain.Groups;
using Domain.Messages;
using Domain.Users;
using Shared;

namespace Application.Messages.GetUserMessage;

internal sealed class GetUserMessageQueryHandler : IQueryHandler<GetUserMessageQuery, IEnumerable<UserMessage>>
{
    private readonly IMessageRepository _messageRepository;
    private readonly IUserRepository _userRepository;

    public GetUserMessageQueryHandler(IMessageRepository messageRepository, IUserRepository userRepository)
    {
        _messageRepository = messageRepository;
        _userRepository = userRepository;
    }

    public async Task<Result<IEnumerable<UserMessage>>> Handle(GetUserMessageQuery request, CancellationToken cancellationToken)
    {
        var receiver = await _userRepository.GetById(request.ReceiverId);
        if (receiver == null) return Result.Failure<IEnumerable<UserMessage>>(UserErrors.NotFound(request.ReceiverId));

        var sender = await _userRepository.GetById(request.SenderId);
        if (sender == null) return Result.Failure<IEnumerable<UserMessage>>(UserErrors.NotFound(request.SenderId));

        var groupMessage = await _messageRepository.GetUserMessages(request.ReceiverId,request.SenderId);
        return Result.Success(groupMessage);
    }
}
