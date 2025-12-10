namespace FlightTickets.Models.Messages;

public abstract class BaseEvent
{
    public Guid MessageId { get; private set; }
    public DateTime Timestamp { get; private set; }
    public string MessageType { get; private set; }

    protected BaseEvent()
    {
        MessageId = Guid.NewGuid();
        Timestamp = DateTime.UtcNow;
        MessageType = GetType().Name;
    }
}
