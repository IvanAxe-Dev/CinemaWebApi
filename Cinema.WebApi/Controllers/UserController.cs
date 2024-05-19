using Cinema.Core.Domain.Entities;
using Cinema.Core.Domain.IdentityEntities;
using Cinema.Core.Domain.RepositoryContracts;
using Cinema.Core.DTO;
using Cinema.Core.Models;
using Cinema.Core.ServiceContracts;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.WebApi.Controllers
{
    [Authorize(Roles = "User")]
    public class UserController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITicketRepository _ticketRepository;
        private readonly IMapper _mapster;
        private readonly IEmailTicketService _emailTicketService;
        public UserController(
            UserManager<ApplicationUser> userManager, IMapper mapster, ITicketRepository ticketRepository, IEmailTicketService emailTicketService)
        {
            _userManager = userManager;
            _mapster = mapster;
            _ticketRepository = ticketRepository;
            _emailTicketService = emailTicketService;
        }

        [HttpGet("[action]")]

        public async Task<List<TicketResponse>> GetUserTickets()
        {
            ApplicationUser? user = await _userManager.FindByNameAsync(User.Identity.Name);

            List<Ticket> userTickets = _ticketRepository.GetWhere(t => t.ApplicationUserId == user.Id).ToList();

            List<TicketResponse> ticketResponses = _mapster.Map<List<TicketResponse>>(userTickets);

            return ticketResponses;
        }

        [HttpPost("[action]/{ticketId:guid}")]
        [Authorize]
        public async Task<ActionResult<TicketResponse>> PostUserTicket([FromRoute]Guid ticketId)
        {
            if (User.Identity==null)
            {
                return BadRequest("user is not found");
            }
            ApplicationUser? user = await _userManager.FindByNameAsync(User.Identity.Name);
            Ticket? userTicket = await _ticketRepository.GetFirstOrDefaultAsync(ticket => ticket.Id == ticketId);
            if (userTicket==null)
            {
                return NotFound("User ticket not found");
            }

            if (userTicket.IsBooked)
            {
                return BadRequest("User ticket is booked");
            }

            userTicket.Session.AvailableTickets--;

            userTicket.ApplicationUserId = user.Id;
            userTicket.IsBooked = true;
            userTicket.BookedAt = DateTime.Now;

            string ticketEmail = BuildTicketMessage(userTicket);


            await _ticketRepository.SaveChangesAsync();

            var message = new Message(new string[] { user.Email! }, "Your Ticket", ticketEmail);
            _emailTicketService.SendEmail(message);

            return _mapster.Map<TicketResponse>(userTicket);
        }

        private string BuildTicketMessage(Ticket ticket)
        {
            return $"Row: {ticket.Row} <br> Column: {ticket.Number} <br> Date: {ticket.Session.Date}<br> Start Time: {ticket.Session.StartTime}<br> Cinema Hall: {ticket.Session.CinemaHall.Graphics}, {ticket.Session.CinemaHall.Privilege} ";
        }
    }
}
