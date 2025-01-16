namespace Shared;

public abstract class Event
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public DateTime TimeStamp { get; set; } = DateTime.UtcNow;
    public string EventType => GetType().Name;
}
