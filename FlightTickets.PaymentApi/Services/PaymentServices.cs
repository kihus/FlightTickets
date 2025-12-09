using FlightTickets.Models.Dtos;
using FlightTickets.Models.Entities;
using FlightTickets.PaymentApi.Services.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace FlightTickets.PaymentApi.Services;

public class PaymentServices(
    IConnectionFactory factory,
    ILogger<PaymentServices> logger
    ) : IPaymentService
{
    private readonly IConnectionFactory _factory = factory;
    private readonly ILogger<PaymentServices> _logger = logger;

    public async Task GetTicketsFromQueueAsync()
    {
        try
        {
            using var connection = await _factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(
                queue: "ticket_order",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null
                );

            _logger.LogInformation("Consume queue...");
            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.ReceivedAsync += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var ticket = JsonSerializer.Deserialize<Ticket>(message)
                    ?? throw new Exception("Ticket not found");

                await ValidatePaymentTicket(ticket);
                _logger.LogInformation("Validated ticket!");
            };

            await channel.BasicConsumeAsync(
                "ticket_order",
                autoAck: true,
                consumer: consumer
                );
        }
        catch (Exception ex)
        {
            _logger.LogError(message: ex.StackTrace);
            throw new Exception(ex.Message, ex);
        }
    }

    private async Task ValidatePaymentTicket(Ticket ticket)
    {
        using var connection = await _factory.CreateConnectionAsync();
        using var channel = await connection.CreateChannelAsync();

        try
        {
            if (ticket.Price > 1000)
            {
                await channel.QueueDeclareAsync(
                    queue: "ticket_approved",
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null
                    );

                var message = JsonSerializer.Serialize(ticket);
                var body = Encoding.UTF8.GetBytes(message);

                await channel.BasicPublishAsync(exchange: string.Empty, routingKey: "ticket_approved", body: body);
            }
            else
            {
                await channel.QueueDeclareAsync(
                    queue: "ticket_denied",
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null
                    );

                var message = JsonSerializer.Serialize(ticket);
                var body = Encoding.UTF8.GetBytes(message);

                await channel.BasicPublishAsync(exchange: string.Empty, routingKey: "ticket_denied", body: body);
            }
        }
        catch (Exception ex)
        {

        }
    }
}
