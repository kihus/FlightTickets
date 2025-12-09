using FlightTickets.Models.Dtos;

namespace FlightTickets.OrderApi.Services.Interfaces;

public interface ITicketService
{
    Task<TicketResponseDto> CreateTicketAsync(TicketRequestDto ticketRequest);
}