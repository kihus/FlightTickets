using FlightTickets.Models.Entities.Enums;
using MongoDB.Bson;

namespace FlightTickets.Models.Entities;

public class Ticket(
    string passengerName,
    string flightNumber,
    string seatNumber,
    decimal price
    )
{
    public ObjectId Id { get; init; } = ObjectId.GenerateNewId();
    public string PassengerName { get; private set; } = passengerName;
    public string FlightNumber { get; private set; } = flightNumber;
    public string SeatNumber { get; private set; } = seatNumber;
    public decimal Price { get; private set; } = price;
    public EStatus Status { get; private set; } = EStatus.Pending;
}
