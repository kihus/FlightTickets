using FlightTickets.PaymentApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FlightTickets.PaymentApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PaymentController(
    ILogger<PaymentController> logger, 
    IPaymentService paymentService
    ) : ControllerBase
{
    private readonly ILogger<PaymentController> _logger = logger;
    private readonly IPaymentService _paymentService = paymentService;

    [HttpGet]
    public async Task<IActionResult> GetTicketAsync()
    {
        try
        {
            _logger.LogInformation("Processing ticket queue...");
            await _paymentService.GetTicketsFromQueueAsync();
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(message: ex.StackTrace);
            return Problem(ex.Message);
        }
    }
}
