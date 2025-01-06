using Application.Abstractions;
using Domain.Groups;
using Domain.Messages;
using Domain.Users;
using Shared;

namespace Application.Messages.GetGroupMessage;
internal sealed class GetGroupMessageQueryHandler : IQueryHandler<GetGroupMessageQuery,IEnumerable<GroupMessage>>
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

    public async Task<Result<IEnumerable<GroupMessage>>> Handle(GetGroupMessageQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetById(request.ReceiverId);
        if (user == null) return Result.Failure<IEnumerable<GroupMessage>>(UserErrors.NotFound(request.ReceiverId));

        var group = await _groupRepository.GetById(request.GroupId);
        if (group == null) return Result.Failure<IEnumerable<GroupMessage>>(GroupErrors.NotFound(request.GroupId));

        var groupMessage = await _messageRepository.GetGroupMessages(request.ReceiverId,request.GroupId);
        return Result.Success(groupMessage);
    }
}
