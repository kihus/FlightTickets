using FlightTickets.Models.Dtos;
using FlightTickets.OrderApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FlightTickets.OrderApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TicketController(
    ILogger<TicketController> logger,
    ITicketService ticketService
    ) : ControllerBase
{
    private readonly ILogger<TicketController> _logger = logger;
    private readonly ITicketService _ticketService = ticketService;

    [HttpPost]
    public async Task<ActionResult<TicketResponseDto>> CreateTicketAsync([FromBody] TicketRequestDto ticket)
    {
        _logger.LogInformation("Creating a new ticket.");

        var createdTicket = await _ticketService.CreateTicketAsync(ticket);
        return Ok(createdTicket);
    }
}
