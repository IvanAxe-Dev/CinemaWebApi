using Cinema.Core.Domain.Entities;
using Cinema.Core.DTO;
using Cinema.Core.ServiceContracts;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionController : BaseController
    {
        private readonly IService<Session> _sessionService;
        private readonly IMapper _mapster;

        public SessionController(IService<Session> sessionService, IMapper mapster)
        {
            _sessionService = sessionService;
            _mapster = mapster;
        }
        
        [HttpGet]
        public async Task<ActionResult<List<Session>>> GetAll()
        {
            List<Session> sessions = await _sessionService.GetAllAsync();

            return Ok(sessions);
        }
        
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Session>> GetById(Guid id)
        {
            Session? session = await _sessionService.FindByIdAsync(id);

            if (session == null)
            {
                return NotFound();
            }

            return Ok(session);
        }
        
        [HttpPost]
        public async Task<ActionResult<Session>> Create(SessionDto sessionDto)
        {
            Session newSession = await _sessionService.Insert(_mapster.Map<Session>(sessionDto));
            return CreatedAtAction(nameof(GetById), new { id = newSession.Id }, newSession);
        }
        
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Session>> Update(Guid id, SessionDto sessionDto)
        {
            Session? existingSession = await _sessionService.FindByIdAsync(id);

            if (existingSession == null)
            {
                return NotFound();
            }

            Session session = _mapster.Map(sessionDto, existingSession);

            return Ok(await _sessionService.Update(session));
        }
        
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
