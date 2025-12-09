using FlightTickets.Models.Entities;

namespace FlightTickets.ConsumerApi.Repositories.Interfaces;

public interface IConsumerRepository
{
    Task SaveTicketApproved(Ticket ticket);
    Task SaveTicketDenied(Ticket ticket);
}