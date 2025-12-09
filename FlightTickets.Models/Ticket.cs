using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FlightTickets.Models;

public class Ticket(
    string passengerName,
    string flightNumber,
    string seatNumber,
    decimal price
    )
{
    public ObjectId Id { get; private set; }
    public string PassengerName { get; private set; } = passengerName;
    public string FlightNumber { get; private set; } = flightNumber;
    public string SeatNumber { get; private set; } = seatNumber;
    public decimal Price { get; private set; } = price;
}
