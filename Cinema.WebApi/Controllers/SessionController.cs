using Cinema.Core.Domain.Entities;
using Cinema.Core.DTO;
using Cinema.Core.ServiceContracts;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.WebApi.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class SessionController : BaseController
    {
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapster;

        public SessionController(ISessionService sessionService, IMapper mapster)
        {
            _sessionService = sessionService;
            _mapster = mapster;
        }
        
        [HttpGet]
        public async Task<ActionResult<List<SessionResponse>>> GetAll()
        {
            List<Session> sessions = await _sessionService.GetAllAsync();

            return Ok(_mapster.Map<List<SessionResponse>>(sessions));
        }
        
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

        //[Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<SessionResponse>> Create(SessionDto sessionDto)
        {
            Session newSession = await _sessionService.Insert(_mapster.Map<Session>(sessionDto));

            Session sessionWithIncludes = await _sessionService.FindByIdAsync(newSession.Id);

            sessionWithIncludes.AvailableSeats =
                (int)sessionWithIncludes.CinemaHall.RowsCount! * (int)sessionWithIncludes.CinemaHall.NumbersCount!;

            await _sessionService.Update(sessionWithIncludes);

            return CreatedAtAction(nameof(GetById), new { id = newSession.Id }, _mapster.Map<SessionResponse>(sessionWithIncludes));
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
