using Application.Abstractions;
using Domain.Groups;
using Domain.Groups.Events;
using Shared;

namespace Application.Groups.Delete;

internal sealed class DeleteGroupCommandHandler : ICommandHandler<DeleteGroupCommand>
{
    private readonly IGroupRepository _groupRepository;
    private readonly IEventStore _eventStore;

    public DeleteGroupCommandHandler(IGroupRepository groupRepository, IEventStore eventStore)
    {
        _groupRepository = groupRepository;
        _eventStore = eventStore;
    }

    public async Task<Result> Handle(DeleteGroupCommand request, CancellationToken cancellationToken)
    {
        var group = await _groupRepository.GetById(request.Id );
        if (group == null) return GroupErrors.NotFound(request.Id);

        var groupDeleted = new GroupDeletedEvent(group.Id,group.Name,group.Description);
        await _groupRepository.DeleteAsync( request.Id );
        await _eventStore.SaveEventAsync(groupDeleted,EntityType.Group);

        return Result.Success();
    }
}
