using Bogus;
using FlightTickets.ConsumerApi.Repositories;
using FlightTickets.Models.Entities;
using Infrastructure.Data.Mongo.Contexts;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using Moq;

namespace Infrastructure.Tests.Repositories.FlightTicketConsumerTests;

public class ConsumerRepositoryTests
{
    private Faker _faker = new Faker("pt_BR");
    private IConfiguration _configuration;
    private MongoDbContext _mongo;
    private ConsumerRepository _consumerRepository;

    public ConsumerRepositoryTests()
    {
        _mongo = new MongoDbContext(_configuration);
        _consumerRepository = new ConsumerRepository(_mongo);
    }

    [Fact]
    public void ConsumeTicketApprovedSavingInDatabase()
    {
        var mockModel = new Mock<ConsumerRepository>();

        mockModel
            .Setup(m => m.SaveTicketApproved(It.IsAny<Ticket>()))
            .Returns(Task.CompletedTask);

        // Arrange
        var passangerName = _faker.Name.FullName();
        var flightNumber = _faker.Random.AlphaNumeric(6).ToLower();
        var seatNumber = $"{_faker.Random.Int(1, 30)}{_faker.Random.Char('A', 'F')}";
        var price = _faker.Finance.Amount(99, 9999);

        // Act
        var ticket = new Ticket(passangerName, flightNumber, seatNumber, price);
        var result = _consumerRepository.SaveTicketApproved(ticket);

        Assert.NotNull(result);
        
    }

}
