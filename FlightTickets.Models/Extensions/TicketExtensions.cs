using FlightTickets.Models.Dtos;
using FlightTickets.Models.Entities;
using FlightTickets.Models.Messages.Events;

namespace FlightTickets.Models.Extensions;

public static class TicketExtensions
{
    public static TicketResponseDto ToDto(this Ticket ticket)
    {
        if (ticket is null)
            return null;

        return new TicketResponseDto
        {
            Id = ticket.Id.ToString(),
            PassengerName = ticket.PassengerName,
            FlightNumber = ticket.FlightNumber,
            SeatNumber = ticket.SeatNumber,
            Price = ticket.Price
        };
    }

    public static TicketCreatedEvent ToEvent(this Ticket ticket)
    {
        if (ticket is null)
            return null;

        return new TicketCreatedEvent
        {
            Id = ticket.Id.ToString(),
            PassengerName = ticket.PassengerName,
            FlightNumber = ticket.FlightNumber,
            SeatNumber = ticket.SeatNumber,
            Price = ticket.Price
        };
    }

}
