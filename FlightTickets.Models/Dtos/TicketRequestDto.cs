using MongoDB.Bson;

namespace FlightTickets.Models.Dtos;

public class TicketRequestDto
{
    public required string PassengerName { get; init; }
    public required string FlightNumber { get; init; } 
    public required string SeatNumber { get; init; } 
    public required decimal Price { get; init; } 
}
