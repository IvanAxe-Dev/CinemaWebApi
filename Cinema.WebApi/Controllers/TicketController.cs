using Cinema.Core.Domain.Entities;
using Cinema.Core.DTO;
using Cinema.Core.ServiceContracts;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.WebApi.Controllers
{
    [Authorize]
    public class TicketController : BaseController
    {
        private readonly ITicketService _ticketService;
        private readonly IMapper _mapster;

        public TicketController(ITicketService ticketService, IMapper mapster)
        {
            _ticketService = ticketService;
            _mapster = mapster;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("[action]")]
        public async Task<ActionResult<List<Ticket>>> GetAllForAdmin()
        {
            List<Ticket> tickets = await _ticketService.GetAllAsync();

            return Ok(tickets);
        }

        //add user identification as in [movie]
        [Authorize(Roles = "ApplicationUser")]
        [HttpGet("[action]")]
        public async Task<ActionResult<List<Ticket>>> GetAllForUser()
        {
            List<Ticket> tickets = await _ticketService.GetAllAsync();

            return Ok(tickets);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("[action]/{id:guid}")]
        public async Task<ActionResult<Ticket>> GetByIdForAdmin(Guid id)
        {
            Ticket? ticket = await _ticketService.FindByIdAsync(id);

            if (ticket == null)
            {
                return NotFound("Ticket not found");
            }

            return Ok(ticket);
        }

        //add user identification check as in [movie]
        [Authorize(Roles = "ApplicationUser")]
        [HttpGet("[action]/{id:guid}")]
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
        [HttpPost("{sessionId:guid}")]
        public async Task<ActionResult<List<TicketResponse>>> CreateRange(Guid sessionId)
        {
            List<TicketResponse> tickets = await _ticketService.CreateTicketsForSession(sessionId);

            return Ok(tickets);
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
