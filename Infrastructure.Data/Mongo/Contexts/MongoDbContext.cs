using FlightTickets.Models.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Infrastructure.Data.Mongo.Contexts;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("MongoDb");
        var databaseName = configuration["DatabaseName"];

        var client = new MongoClient(connectionString);
        _database = client.GetDatabase(databaseName);
    }

    public IMongoCollection<Ticket> ApprovedTickets
        => _database.GetCollection<Ticket>("ApprovedTickets");

    public IMongoCollection<Ticket> DeniedTickets
        => _database.GetCollection<Ticket>("DeniedTickets");
}
