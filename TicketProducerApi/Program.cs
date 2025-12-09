using FlightTickets.ConsumerApi.Repositories;
using FlightTickets.ConsumerApi.Repositories.Interfaces;
using FlightTickets.ConsumerApi.Services;
using FlightTickets.ConsumerApi.Services.Interfaces;
using Infrastructure.Data.Mongo.Contexts;
using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddSingleton<MongoDbContext>();

builder.Services.AddSingleton<IConnectionFactory>(con =>
{
    var factory = new ConnectionFactory { HostName = "localhost" };
    return factory;
});

builder.Services.AddScoped<IConsumerRepository, ConsumerRepository>();
builder.Services.AddScoped<IConsumerService, ConsumerService>();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
