using Cinema.Core.Domain.Entities;
using Cinema.Core.Domain.IdentityEntities;
using Cinema.Core.Domain.RepositoryContracts;
using Cinema.Core.DTO;
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

        public UserController(
            UserManager<ApplicationUser> userManager, IMapper mapster, ITicketRepository ticketRepository)
        {
            _userManager = userManager;
            _mapster = mapster;
            _ticketRepository = ticketRepository;
        }

        [HttpGet("[action]")]

        public async Task<List<TicketResponse>> GetUserTickets()
        {
            ApplicationUser? user = await _userManager.FindByNameAsync(User.Identity.Name);

            List<Ticket> userTickets = _ticketRepository.GetWhere(t => t.ApplicationUserId == user.Id).ToList();

            List<TicketResponse> ticketResponses = _mapster.Map<List<TicketResponse>>(userTickets);

            return ticketResponses;
        }
    }
}
