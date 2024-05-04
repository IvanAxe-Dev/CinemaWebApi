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
            var sessions = await _sessionService.GetAllAsync();
            return Ok(sessions);
        }
        
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Session>> GetById(Guid id)
        {
            var session = await _sessionService.FindByIdAsync(id);
            if (session == null)
            {
                return NotFound();
            }
            return Ok(session);
        }
        
        [HttpPost]
        public async Task<ActionResult<Session>> Create(SessionDto sessionDto)
        {
            var session = _mapster.Map<Session>(sessionDto);
            await _sessionService.Insert(session);
            return CreatedAtAction(nameof(GetById), new { id = session.Id }, session);
        }
        
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Session>> Update(Guid id, SessionDto sessionDto)
        {
            var existingSession = await _sessionService.FindByIdAsync(id);
            if (existingSession == null)
            {
                return NotFound();
            }
            var session = _mapster.Map(sessionDto, existingSession);
            var updatedSession = await _sessionService.Update(session);
            return Ok(updatedSession);
        }
        
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var session = await _sessionService.FindByIdAsync(id);
            if (session == null)
            {
                return NotFound();
            }
            await _sessionService.DeleteAsync(session);
            return NoContent();
        }
    }
}
