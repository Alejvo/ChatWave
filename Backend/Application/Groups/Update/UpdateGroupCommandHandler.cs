using Application.Abstractions;
using Domain.Groups;
using Domain.Groups.Events;
using Shared;
using System.Windows.Input;

namespace Application.Groups.Update;

internal sealed class UpdateGroupCommandHandler : ICommandHandler<UpdateGroupCommand>
{
    private readonly IGroupRepository _groupRepository;
    private readonly IEventStore _eventStore;

    public UpdateGroupCommandHandler(IGroupRepository groupRepository, IEventStore eventStore)
    {
        _groupRepository = groupRepository;
        _eventStore = eventStore;
    }

    public async Task<Result> Handle(UpdateGroupCommand request, CancellationToken cancellationToken)
    {
        byte[] imageBytes = null;
        var group = await _groupRepository.GetById(request.Id);

        if (group == null) return GroupErrors.NotFound(request.Id);

        if (request.Image != null)
        {
            using (var memoryStream = new MemoryStream())
            {
                await request.Image.CopyToAsync(memoryStream);
                imageBytes = memoryStream.ToArray();
            }
        }

        var updatedGroup = GroupRequest.Create(
            request.Id,
            request.Name,
            request.Description,
            imageBytes
        );
        var updatedGroupEvent = new GroupUpdatedEvent(request.Id,request.Name,request.Description);

        await _groupRepository.UpdateAsync(updatedGroup);
        await _eventStore.SaveEventAsync(updatedGroupEvent, EntityType.Group);

        return Result.Success();
    }
}
