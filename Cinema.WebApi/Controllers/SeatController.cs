using Cinema.Core.Domain.Entities;
using Cinema.Core.DTO;
using Cinema.Core.ServiceContracts;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeatController : ControllerBase
    {
        private readonly ISeatService _seatService;
        private readonly IMapper _mapster;

        public SeatController(ISeatService seatService, IMapper mapster)
        {
            _seatService = seatService;
            _mapster = mapster;
        }
        
        [HttpGet]
        public async Task<ActionResult<List<Seat>>> GetAll()
        {
            List<Seat> seats = await _seatService.GetAllAsync();

            return Ok(seats);
        }
        
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Seat>> GetById(Guid id)
        {
            Seat? seat = await _seatService.FindByIdAsync(id);

            if (seat == null)
            {
                return NotFound("Seat not found");
            }

            return Ok(seat);
        }
        
        [HttpPost]
        public async Task<ActionResult<Seat>> Create(SeatDto seatDto)
        {
            Seat newSeat = await _seatService.Insert(_mapster.Map<Seat>(seatDto));

            return CreatedAtAction(nameof(GetById), new { id = newSeat.Id }, newSeat);
        }

        [HttpPost("{sessionId:guid}")]
        public async Task<ActionResult<List<SeatResponse>>> CreateRange(Guid sessionId)
        {
            var seats = await _seatService.CreateSeatsForSession(sessionId);

            return Ok(seats);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Seat>> Update(Guid id, SeatDto seatDto)
        {
            Seat? existingSeat = await _seatService.FindByIdAsync(id);

            if (existingSeat == null)
            {
                return NotFound("Seat not found");
            }

            Seat seat = _mapster.Map(seatDto, existingSeat);

            return Ok(await _seatService.Update(seat));
        }
        
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            Seat? seat = await _seatService.FindByIdAsync(id);

            if (seat == null)
            {
                return NotFound("Seat not found");
            }

            await _seatService.DeleteAsync(seat);

            return NoContent();
        }
    }
}
