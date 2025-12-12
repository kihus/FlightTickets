using FlightTickets.PaymentApi.Services;
using FlightTickets.PaymentApi.Services.Interfaces;
using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddSingleton<IConnectionFactory>(con =>
{
    var factory = new ConnectionFactory { HostName = "localhost" };
    return factory;
});
builder.Services.AddSingleton<IPaymentService, PaymentService>();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
