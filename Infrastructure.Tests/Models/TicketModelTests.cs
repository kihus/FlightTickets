using Bogus;
using FlightTickets.Models.Entities;
using Moq;

namespace Infrastructure.Tests.Models
{
    public class TicketModelTests
    {
        private readonly Faker _faker = new("pt_BR");
        

        [Fact]
        public void TestingTicketConstructorWithAllParams()                                                                                     
        {
            // Arrange
            var passangerName = _faker.Name.FullName();
            var flightNumber = _faker.Random.AlphaNumeric(6).ToLower();
            var seatNumber = $"{_faker.Random.Int(1, 30)}{_faker.Random.Char('A', 'F')}";
            var price = _faker.Finance.Amount(99, 9999);

            // Act
            var ticket = new Ticket(passangerName, flightNumber, seatNumber, price);

            // Assert
            Assert.NotNull(ticket);
            Assert.Equal(passangerName, ticket.PassengerName);
            Assert.Equal(flightNumber, ticket.FlightNumber);
            Assert.Equal(seatNumber, ticket.SeatNumber);
            Assert.Equal(price, ticket.Price);

        }
    }
}
