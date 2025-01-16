using Application.Abstractions;
using Domain.Groups;
using Domain.Groups.Events;
using Shared;

namespace Application.Groups.Create;

internal sealed class CreateGroupCommandHandler : ICommandHandler<CreateGroupCommand>
{
    private readonly IGroupRepository _groupRepository;
    private readonly IEventStore _eventStore;

    public CreateGroupCommandHandler(IGroupRepository groupRepository, IEventStore eventStore)
    {
        _groupRepository = groupRepository;
        _eventStore = eventStore;
    }

    public async Task<Result> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
    {
        byte[] imageBytes = null;
        if (request.Image != null)
        {
            using (var memoryStream = new MemoryStream())
            {
                await request.Image.CopyToAsync(memoryStream);
                imageBytes = memoryStream.ToArray();
            }
        }
        var group = GroupRequest.Create(request.Name,request.Description,imageBytes);
        var groupCreated = new GroupCreatedEvent(group.Id,group.Name,group.Description);

        await _groupRepository.CreateAsync(group);
        await _eventStore.SaveEventAsync(groupCreated,EntityType.Group);

        return Result.Success();
    }
}
