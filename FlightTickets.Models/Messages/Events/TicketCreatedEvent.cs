namespace FlightTickets.Models.Messages.Events;

public class TicketCreatedEvent : BaseEvent
{
    public required string Id { get; init; }
    public required string PassengerName { get; init; } 
    public required string FlightNumber { get; init; }
    public required string SeatNumber { get; init; } 
    public required decimal Price { get; init; }
}
