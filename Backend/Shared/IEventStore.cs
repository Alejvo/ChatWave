using MongoDB.Driver;

namespace Shared;

public interface IEventStore
{
    Task SaveEventAsync<T>(T @event, EntityType entityType) where T : Event;
    Task<List<T>> GetAllEventsAsync<T>(EntityType entityType) where T : Event;
    Task<List<T>> GetEventsByFilterAsync<T>(EntityType entityType, FilterDefinition<T> filter) where T : Event;
}
