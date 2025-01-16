using Application.Abstractions;
using Domain.Users;
using Domain.Users.Events;
using Shared;

namespace Application.Users.Delete;

internal sealed class DeleteUserCommandHandler : ICommandHandler<DeleteUserCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IEventStore _eventStore;

    public DeleteUserCommandHandler(IUserRepository userRepository, IEventStore eventStore)
    {
        _userRepository = userRepository;
        _eventStore = eventStore;
    }

    public async Task<Result> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetById(request.Id);

        if (user == null) return UserErrors.NotFound(request.Id);

        var deletedUser = new UserDeletedEvent(user.Id,$"{user.FirstName} {user.LastName}",user.Email,user.Username);

        await _userRepository.DeleteAsync(user.Id);

        await _eventStore.SaveEventAsync(deletedUser,EntityType.User);
        return Result.Success();
    }
}
