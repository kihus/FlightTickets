using Bogus;
using FlightTickets.Models.Dtos;
using FlightTickets.OrderApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.Tests.Services.FlightTicketsOrderTests;

public class FlightTicketsOrderService
{
    private TicketService _ticketService;
    private Faker _faker = new Faker();
    public FlightTicketsOrderService()
    {
        _ticketService = new TicketService();
    }

    [Fact]
    public void TestingServiceToCreateATicketAndSendToQueue()
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

        var result = _ticketService.CreateTicketAsync(ticketRequest).Result;

        Assert.NotNull(result);
        Assert.IsType<TicketResponseDto>(result);
    }
}
