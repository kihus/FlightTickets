using FlightTickets.PaymentApi.Controllers;
using FlightTickets.PaymentApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace Infrastructure.Tests.Controllers.PaymentControllerTests;

public class PaymentControllerTests
{
    private ILogger<PaymentController> _loggerController;
    private ILogger<PaymentService> _loggerService;
    private IConnectionFactory _factory;
    private PaymentService _paymentService;
    private PaymentController _paymentController;

    public PaymentControllerTests()
    {
        _loggerController = new LoggerFactory().CreateLogger<PaymentController>();
        _loggerService = new LoggerFactory().CreateLogger<PaymentService>();
        _factory = new ConnectionFactory { HostName = "localhost" };
        _paymentService = new PaymentService(_factory, _loggerService);
        _paymentController = new PaymentController(_loggerController, _paymentService);
    }

    [Fact]
    [Trait("HttpVerbs", "Get")]
    public void ProcessPaymentMustReturnOkResult()
    {
        // Act
        var result = _paymentController.GetTicketAsync().Result;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkResult>(result);
    }
}
