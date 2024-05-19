using Cinema.Core.Domain.Entities;
using Cinema.Core.Domain.IdentityEntities;
using Cinema.Core.DTO;
using Cinema.Core.ServiceContracts;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.WebApi.Controllers
{
    
    public class TicketController : BaseController
    {
        private readonly ITicketService _ticketService;
        private readonly IMapper _mapster;
        private readonly UserManager<ApplicationUser> _userManager;

        public TicketController(ITicketService ticketService, IMapper mapster, UserManager<ApplicationUser> userManager)
        {
            _ticketService = ticketService;
            _mapster = mapster;
            _userManager = userManager;
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<List<TicketResponse>>> GetAllTickets()
        {
            List<Ticket> tickets = await _ticketService.GetAllAsync();

            var responseTickets = _mapster.Map<List<TicketResponse>>(tickets);

            return Ok(responseTickets);
        }

        //add user identification check as in [movie]
        [Authorize(Roles = "User")]
        [HttpGet("[action]/{id:guid}")]
        public async Task<ActionResult<TicketResponse>> GetByIdForUser(Guid id)
        {
            Ticket? ticket = await _ticketService.FindByIdAsync(id);

            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            if (ticket == null || user.Id != ticket.ApplicationUserId)
            {
                return NotFound("Ticket not found");
            }

            return Ok(_mapster.Map<TicketResponse>(ticket));
        }

        //add user identification
        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<Ticket>> Create(TicketDto ticketDto)
        {
            
            Ticket newTicket = await _ticketService.Insert(_mapster.Map<Ticket>(ticketDto));

            return CreatedAtAction(nameof(GetByIdForUser), new { id = newTicket.Id }, newTicket);
        }

        //add user identification
        [Authorize(Roles = "Admin")]
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<TicketResponse>> Update(Guid id, TicketUpdateRequest ticketDto)
        {
            Ticket? existingTicket = await _ticketService.FindByIdAsync(id);

            if (existingTicket == null)
            {
                return NotFound("Ticket not found");
            }

            Ticket ticket = _mapster.Map(ticketDto, existingTicket);

            await _ticketService.Update(ticket);

            return Ok(_mapster.Map<TicketResponse>(ticket));
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
