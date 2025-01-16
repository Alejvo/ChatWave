namespace Shared;

public abstract class Aggregate
{
    private readonly List<Event> _domainEvents = new();

    protected Aggregate(string id)
    {
        Id = id;
    }

    protected Aggregate()
    {
    }
    public string Id { get; protected set; }

    public List<Event> DomainEvents => _domainEvents.ToList();

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    protected void Raise(Event domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

}

