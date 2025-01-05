using Application.Abstractions;
using Domain.Groups;
using Shared;

namespace Application.Groups.Delete;

internal sealed class DeleteGroupCommandHandler : ICommandHandler<DeleteGroupCommand>
{
    private readonly IGroupRepository _groupRepository;

    public DeleteGroupCommandHandler(IGroupRepository groupRepository)
    {
        _groupRepository = groupRepository;
    }

    public async Task<Result> Handle(DeleteGroupCommand request, CancellationToken cancellationToken)
    {
        var group = await _groupRepository.GetById(request.Id );
        if (group == null) return GroupErrors.NotFound(request.Id);

        await _groupRepository.DeleteAsync( request.Id );
        return Result.Success();
    }
}
