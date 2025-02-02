using Application.Abstractions;
using Application.Messages.Common;
using Domain.Groups;
using Domain.Messages;
using Domain.Users;
using Shared;

namespace Application.Messages.GetGroupMessage;
internal sealed class GetGroupMessageQueryHandler : IQueryHandler<GetGroupMessageQuery,IEnumerable<MessageResponse>>
{
    private readonly IMessageRepository _messageRepository;
    private readonly IUserRepository _userRepository;
    private readonly IGroupRepository _groupRepository;

    public GetGroupMessageQueryHandler(
        IMessageRepository messageRepository, 
        IUserRepository userRepository, 
        IGroupRepository groupRepository)
    {
        _messageRepository = messageRepository;
        _userRepository = userRepository;
        _groupRepository = groupRepository;
    }

    public async Task<Result<IEnumerable<MessageResponse>>> Handle(GetGroupMessageQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetById(request.ReceiverId);
        if (user == null) return Result.Failure<IEnumerable<MessageResponse>>(UserErrors.NotFound(request.ReceiverId));

        var group = await _groupRepository.GetById(request.GroupId);
        if (group == null) return Result.Failure<IEnumerable<MessageResponse>>(GroupErrors.NotFound(request.GroupId));

        var groupMessage = await _messageRepository.GetGroupMessages(request.ReceiverId,request.GroupId);
        var messages = groupMessage.Select(message => message.ToMessageResponse(request.ReceiverId));

        return Result.Success(messages);
    }
}
