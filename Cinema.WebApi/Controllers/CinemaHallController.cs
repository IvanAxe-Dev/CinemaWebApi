using Cinema.Core.Domain.Entities;
using Cinema.Core.DTO;
using Cinema.Core.ServiceContracts;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
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
            var cinemaHalls = await _cinemaHallService.GetAllAsync();
            return Ok(cinemaHalls);
        }
        
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<CinemaHall>> GetById(Guid id)
        {
            var cinemaHall = await _cinemaHallService.FindByIdAsync(id);
            if (cinemaHall == null)
            {
                return NotFound("Cinema hall not found");
            }
            return Ok(cinemaHall);
        }
        
        [HttpPost]
        public async Task<ActionResult<CinemaHall>> Create(CinemaHallDto cinemaHallDto)
        {
            var newCinemaHall = _mapster.Map<CinemaHall>(cinemaHallDto);
            var createdCinemaHall = await _cinemaHallService.Insert(newCinemaHall);
            return CreatedAtAction(nameof(GetById), new { id = createdCinemaHall.Id }, createdCinemaHall);
        }
        
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<CinemaHall>> Update(Guid id, CinemaHallDto cinemaHallDto)
        {
            var existingCinemaHall = await _cinemaHallService.FindByIdAsync(id);
            if (existingCinemaHall == null)
            {
                return NotFound("Cinema hall not found");
            }
            var cinemaHall = _mapster.Map(cinemaHallDto, existingCinemaHall);
            var updatedCinemaHall = await _cinemaHallService.Update(cinemaHall);
            return Ok(updatedCinemaHall);
        }
        
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var cinemaHall = await _cinemaHallService.FindByIdAsync(id);
            if (cinemaHall == null)
            {
                return NotFound("Cinema hall not found");
            }
            await _cinemaHallService.DeleteAsync(cinemaHall);
            return NoContent();
        }
    }
}
