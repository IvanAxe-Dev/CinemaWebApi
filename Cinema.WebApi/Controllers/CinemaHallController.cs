using Cinema.Core.Domain.Entities;
using Cinema.Core.DTO;
using Cinema.Core.ServiceContracts;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CinemaHallController : BaseController
    {
        private readonly IService<CinemaHall> _cinemaHallService;
        private readonly IMapper _mapster;

        public CinemaHallController(IService<CinemaHall> cinemaHallService, IMapper mapster)
        {
            _cinemaHallService = cinemaHallService;
            _mapster = mapster;
        }
        
        [HttpGet]
        public async Task<ActionResult<List<CinemaHall>>> GetAll()
        {
            List<CinemaHall> cinemaHalls = await _cinemaHallService.GetAllAsync();

            return Ok(cinemaHalls);
        }
        
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<CinemaHall>> GetById(Guid id)
        {
            CinemaHall? cinemaHall = await _cinemaHallService.FindByIdAsync(id);

            if (cinemaHall == null)
            {
                return NotFound("Cinema hall not found");
            }

            return Ok(cinemaHall);
        }
        
        [HttpPost]
        public async Task<ActionResult<CinemaHall>> Create(CinemaHallDto cinemaHallDto)
        {
            CinemaHall newCinemaHall = await _cinemaHallService.Insert(_mapster.Map<CinemaHall>(cinemaHallDto));

            return CreatedAtAction(nameof(GetById), new { id = newCinemaHall.Id }, newCinemaHall);
        }
        
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<CinemaHall>> Update(Guid id, CinemaHallDto cinemaHallDto)
        {
            CinemaHall? existingCinemaHall = await _cinemaHallService.FindByIdAsync(id);

            if (existingCinemaHall == null)
            {
                return NotFound("Cinema hall not found");
            }

            CinemaHall cinemaHall = _mapster.Map(cinemaHallDto, existingCinemaHall);

            return Ok(await _cinemaHallService.Update(cinemaHall));
        }
        
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            CinemaHall? cinemaHall = await _cinemaHallService.FindByIdAsync(id);

            if (cinemaHall == null)
            {
                return NotFound("Cinema hall not found");
            }

            await _cinemaHallService.DeleteAsync(cinemaHall);
            return NoContent();
        }
    }
}
