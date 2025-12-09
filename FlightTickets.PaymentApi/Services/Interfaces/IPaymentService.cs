
namespace FlightTickets.PaymentApi.Services.Interfaces;

public interface IPaymentService
{
    Task GetTicketsFromQueueAsync();
}