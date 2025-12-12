using Bogus;
using FlightTickets.Models.Dtos;
using FlightTickets.OrderApi.Controllers;
using FlightTickets.OrderApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Tests.Controllers.FlightTicketsOrderTests;

public class FlightTicketsOrderTests
{
    private readonly Faker _faker = new Faker("pt_BR");
    private ILogger<TicketController> _loggerController;
    private TicketController _ticketController;
    private TicketService _ticketService;

    public FlightTicketsOrderTests()
    {
        _loggerController = new LoggerFactory().CreateLogger<TicketController>();
        _ticketService = new TicketService();
        _ticketController = new TicketController(_loggerController, _ticketService);
    }

    [Fact]
    [Trait("HttpVerbs", "Post")]
    public void CreatingTicketTest()
    {
        var passangerName = _faker.Name.FullName();
        var flightNumber = _faker.Random.AlphaNumeric(6).ToLower();
        var seatNumber = $"{_faker.Random.Int(1, 30)}{_faker.Random.Char('A', 'F')}";
        var price = _faker.Finance.Amount(99, 9999);

        var ticketRequest = new TicketRequestDto
        {
            PassengerName = passangerName,
            FlightNumber = flightNumber,
            SeatNumber = seatNumber,
            Price = price
        };

        var result = _ticketController.CreateTicketAsync(ticketRequest).Result;

        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    [Trait("HttpVerbs", "Post")]
    public void Carga()
    {
        var count = 0;
        var result = new TicketResponseDto();

        while (count < 1000000)
        {
            var passangerName = _faker.Name.FullName();
            var flightNumber = _faker.Random.AlphaNumeric(6).ToLower();
            var seatNumber = $"{_faker.Random.Int(1, 30)}{_faker.Random.Char('A', 'F')}";
            var price = _faker.Finance.Amount(99, 9999);

            var ticketRequest = new TicketRequestDto
            {
                PassengerName = passangerName,
                FlightNumber = flightNumber,
                SeatNumber = seatNumber,
                Price = price
            };

            var ticket = _ticketController.CreateTicketAsync(ticketRequest).Result;
            result = ticket.Value;
        }

        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
    }
}
