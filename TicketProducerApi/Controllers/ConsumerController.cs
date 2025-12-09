using FlightTickets.ConsumerApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FlightTickets.ConsumerApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ConsumerController(
    IConsumerService consumerService
    ) : ControllerBase
{
    private readonly IConsumerService _consumerService = consumerService;

    [HttpGet]
    public async Task<ActionResult> SaveTicket()
    {
        try
        {
            await _consumerService.SaveTickets();
            return Ok();
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
    }
}
