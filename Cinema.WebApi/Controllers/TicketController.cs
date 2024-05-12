using Cinema.Core.Domain.Entities;
using Cinema.Core.DTO;
using Cinema.Core.ServiceContracts;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _ticketService;
        private readonly IMapper _mapster;

        public TicketController(ITicketService ticketService, IMapper mapster)
        {
            _ticketService = ticketService;
            _mapster = mapster;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<List<Ticket>>> GetAllForAdmin()
        {
            List<Ticket> tickets = await _ticketService.GetAllAsync();

            return Ok(tickets);
        }

        //add user identification as in [movie]
        [Authorize(Roles = "User")]
        [HttpGet]
        public async Task<ActionResult<List<Ticket>>> GetAllForUser()
        {
            List<Ticket> tickets = await _ticketService.GetAllAsync();

            return Ok(tickets);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Ticket>> GetAllForAdmin(Guid id)
        {
            Ticket? ticket = await _ticketService.FindByIdAsync(id);

            if (ticket == null)
            {
                return NotFound("Ticket not found");
            }

            return Ok(ticket);
        }

        //add user identification check as in [movie]
        [Authorize(Roles = "User")]
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Ticket>> GetByIdForUser(Guid id)
        {
            Ticket? ticket = await _ticketService.FindByIdAsync(id);

            if (ticket == null)
            {
                return NotFound("Ticket not found");
            }

            return Ok(ticket);
        }

        //add user identification
        [HttpPost]
        public async Task<ActionResult<Ticket>> Create(TicketDto ticketDto)
        {
            Ticket newTicket = await _ticketService.Insert(_mapster.Map<Ticket>(ticketDto));

            return CreatedAtAction(nameof(GetByIdForUser), new { id = newTicket.Id }, newTicket);
        }

        //add user identification
        [Authorize(Roles = "Admin")]
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Ticket>> Update(Guid id, TicketDto ticketDto)
        {
            Ticket? existingTicket = await _ticketService.FindByIdAsync(id);

            if (existingTicket == null)
            {
                return NotFound("Ticket not found");
            }

            Ticket ticket = _mapster.Map(ticketDto, existingTicket);

            return Ok(await _ticketService.Update(ticket));
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            Ticket? existingTicket = await _ticketService.FindByIdAsync(id);

            if (existingTicket == null)
            {
                return NotFound("Ticket not found");
            }

            await _ticketService.DeleteAsync(existingTicket);

            return NoContent();
        }
    }
}
