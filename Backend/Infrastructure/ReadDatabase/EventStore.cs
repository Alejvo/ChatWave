using Domain.Users;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Shared;

namespace Infrastructure.ReadDatabase;

public sealed class EventStore : IEventStore
{
    private readonly IMongoDatabase _database;

    public EventStore(IOptions<MongoDBSettings> configuration)
    {
        var mongoClient = new MongoClient(configuration.Value.ConnectionString);
        _database = mongoClient.GetDatabase(configuration.Value.DatabaseName);
    }

    private IMongoCollection<T> GetCollection<T>(EntityType entityType) where T : Event
    {
        var collectionName = $"{entityType}Events";
        return _database.GetCollection<T>(collectionName);
    }

    public async Task<List<T>> GetAllEventsAsync<T>(EntityType entityType) where T : Event
    {
        var collection = GetCollection<T>(entityType);
        return await collection.Find(_ => true).ToListAsync();
    }

    public async Task<List<T>> GetEventsByFilterAsync<T>(EntityType entityType, FilterDefinition<T> filter) where T : Event
    {
        var collection = GetCollection<T>(entityType);
        return await collection.Find(filter).ToListAsync();
    }

    public async Task SaveEventAsync<T>(T @event, EntityType entityType) where T : Event
    {
        try
        {
            var collection = GetCollection<T>(entityType);
            await collection.InsertOneAsync(@event);
            Console.WriteLine("Documento insertado con éxito.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al insertar el documento: {ex.Message}");
        }

    }
}
