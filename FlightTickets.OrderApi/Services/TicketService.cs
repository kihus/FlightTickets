using FlightTickets.Models.Dtos;
using FlightTickets.Models.Entities;
using FlightTickets.Models.Extensions;
using FlightTickets.OrderApi.Services.Interfaces;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace FlightTickets.OrderApi.Services;

public class TicketService : ITicketService
{
    public async Task<TicketResponseDto> CreateTicketAsync(TicketRequestDto ticketRequest)
    {
        try
        {
            var newTicket = new Ticket(
                ticketRequest.PassengerName,
                ticketRequest.FlightNumber,
                ticketRequest.SeatNumber,
                ticketRequest.Price
                );

            var factory = new ConnectionFactory { HostName = "localhost" };

            using var connection = await factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(
                queue: "ticket_order",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null
                );

            var ticketSerialize = JsonSerializer.Serialize(newTicket.ToEvent());
            var body = Encoding.UTF8.GetBytes(ticketSerialize);

            await channel.BasicPublishAsync(
                exchange: string.Empty, 
                routingKey: "ticket_order", 
                body: body
                );

            return newTicket.ToDto();   
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
