using Application.Abstractions;
using Domain.Groups;
using Shared;
using System.Windows.Input;

namespace Application.Groups.Update;

internal sealed class UpdateGroupCommandHandler : ICommandHandler<UpdateGroupCommand>
{
    private readonly IGroupRepository _groupRepository;

    public UpdateGroupCommandHandler(IGroupRepository groupRepository)
    {
        _groupRepository = groupRepository;
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
            request.Name,
            request.Description,
            imageBytes
        );

        await _groupRepository.UpdateAsync(updatedGroup);

        return Result.Success();
    }
}
