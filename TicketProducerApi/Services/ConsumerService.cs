using FlightTickets.ConsumerApi.Repositories.Interfaces;
using FlightTickets.ConsumerApi.Services.Interfaces;
using FlightTickets.Models.Entities;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System.Text;
using System.Text.Json;

namespace FlightTickets.ConsumerApi.Services;

public class ConsumerService(
    ILogger<ConsumerService> logger,
    IConsumerRepository consumerRepository,
    IConnectionFactory factory
    ) : IConsumerService
{
    private readonly ILogger<ConsumerService> _logger = logger;
    private readonly IConsumerRepository _consumerRepository = consumerRepository;
    private readonly IConnectionFactory _factory = factory;
    public async Task SaveTickets()
    {
        try
        {
            using var connection = await _factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();

            _logger.LogInformation("Saving approved tickets...");
            await SaveTicketApproved(connection, channel);

            _logger.LogInformation("Saving denied tickets...");
            await SaveTicketDenied(connection, channel);
        }
        catch (RabbitMQClientException rabbitEx)
        {
            _logger.LogError(rabbitEx.StackTrace);
            throw new Exception("Rabbit Exception: " + rabbitEx.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.StackTrace);
            throw new Exception(ex.Message);
        }

    }

    private async Task SaveTicketApproved(IConnection connection, IChannel channel)
    {
        await channel.QueueDeclareAsync(queue: "ticket_approved", durable: false, exclusive: false, autoDelete: false, arguments: null);

        var consumer = new AsyncEventingBasicConsumer(channel);
        consumer.ReceivedAsync += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            var ticketApproved = JsonSerializer.Deserialize<Ticket>(message);
            await _consumerRepository.SaveTicketApproved(ticketApproved);
        };

        await channel.BasicConsumeAsync(queue: "ticket_approved", autoAck: true, consumer: consumer);
    }
    private async Task SaveTicketDenied(IConnection connection, IChannel channel)
    {
        await channel.QueueDeclareAsync(queue: "ticket_denied", durable: false, exclusive: false, autoDelete: false, arguments: null);

        var consumer = new AsyncEventingBasicConsumer(channel);
        consumer.ReceivedAsync += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            var ticketApproved = JsonSerializer.Deserialize<Ticket>(message);
            await _consumerRepository.SaveTicketDenied(ticketApproved);
        };

        await channel.BasicConsumeAsync(queue: "ticket_denied", autoAck: true, consumer: consumer);
    }
}
