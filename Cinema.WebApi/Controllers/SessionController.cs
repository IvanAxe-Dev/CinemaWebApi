using Cinema.Core.Domain.Entities;
using Cinema.Core.DTO;
using Cinema.Core.ServiceContracts;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionController : BaseController
    {
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapster;
        private readonly ICinemaHallService _cinemaHallService;

        public SessionController(ISessionService sessionService, IMapper mapster, ICinemaHallService cinemaHallService)
        {
            _sessionService = sessionService;
            _mapster = mapster;
            _cinemaHallService = cinemaHallService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<List<SessionResponse>>> GetAll()
        {
            List<Session> sessions = await _sessionService.GetAllAsync();

            return Ok(_mapster.Map<List<SessionResponse>>(sessions));
        }

        [AllowAnonymous]
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<SessionResponse>> GetById(Guid id)
        {
            Session? session = await _sessionService.FindByIdAsync(id);

            if (session == null)
            {
                return NotFound();
            }
            
            var sessionResponse = _mapster.Map<SessionResponse>(session);

            return Ok(sessionResponse);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<SessionResponse>> Create(SessionDto sessionDto)
        {
            var sessionCinemaHall = await _cinemaHallService.FindByIdAsync(sessionDto.CinemaHallId);

            var newSession = _mapster.Map<Session>(sessionDto);
            newSession.AvailableTickets = (int)(sessionCinemaHall.RowsCount * sessionCinemaHall.NumbersCount)!;

            await _sessionService.Insert(newSession);

            return CreatedAtAction(nameof(GetById), new { id = newSession.Id }, _mapster.Map<SessionResponse>(newSession));
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<SessionResponse>> Update(Guid id, SessionDto sessionDto)
        {
            Session? existingSession = await _sessionService.FindByIdAsync(id);

            if (existingSession == null)
            {
                return NotFound();
            }

            Session session = _mapster.Map(sessionDto, existingSession);

            return Ok(_mapster.Map<SessionResponse>(await _sessionService.Update(session)));
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            Session? session = await _sessionService.FindByIdAsync(id);

            if (session == null)
            {
                return NotFound();
            }

            await _sessionService.DeleteAsync(session);
            return NoContent();
        }
    }
}
