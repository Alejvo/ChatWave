using Application.Abstractions;
using Domain.Groups;
using Shared;

namespace Application.Groups.Create;

internal sealed class CreateGroupCommandHandler : ICommandHandler<CreateGroupCommand>
{
    private readonly IGroupRepository _groupRepository;

    public CreateGroupCommandHandler(IGroupRepository groupRepository)
    {
        _groupRepository = groupRepository;
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

        await _groupRepository.CreateAsync(group);
        return Result.Success();
    }
}
