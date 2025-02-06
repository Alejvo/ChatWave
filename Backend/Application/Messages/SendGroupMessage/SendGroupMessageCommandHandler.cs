using Application.Abstractions;
using Application.Messages.Common;
using Domain.Groups;
using Domain.Messages;
using Domain.Users;
using Shared;

namespace Application.Messages.SendGroupMessage;

internal sealed class SendGroupMessageCommandHandler : ICommandHandler<SendGroupMessageCommand,MessageResponse>
{
    private readonly IMessageRepository _messageRepository;
    private readonly IGroupRepository _groupRepository;
    private readonly IUserRepository _userRepository;

    public SendGroupMessageCommandHandler(IMessageRepository messageRepository, IGroupRepository groupRepository, IUserRepository userRepository)
    {
        _messageRepository = messageRepository;
        _groupRepository = groupRepository;
        _userRepository = userRepository;
    }

    public async Task<Result<MessageResponse>> Handle(SendGroupMessageCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetById(request.UserId);
        if (user == null) return Result.Failure<MessageResponse>(UserErrors.NotFound(request.GroupId));

        var group = await _groupRepository.GetById(request.GroupId);
        if (group == null) return Result.Failure<MessageResponse>(GroupErrors.NotFound(request.GroupId));

        var id = Guid.NewGuid().ToString();
        var message = GroupMessage.Create(id, request.Text, request.UserId, request.GroupId, request.SentAt);

        await _messageRepository.SendToGroup(message);
        return Result.Success(message.ToMessageResponse(user.Id));
    }
}
