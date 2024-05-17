using Cinema.Core.Domain.Entities;
using Cinema.Core.DTO;
using Cinema.Core.ServiceContracts;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Update.Internal;

namespace Cinema.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CinemaHallController : BaseController
    {
        private readonly ICinemaHallService _cinemaHallService;
        private readonly IMapper _mapster;

        public CinemaHallController(ICinemaHallService cinemaHallService, IMapper mapster)
        {
            _cinemaHallService = cinemaHallService;
            _mapster = mapster;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<List<CinemaHallResponse>>> GetAll()
        {
            List<CinemaHall> cinemaHalls = await _cinemaHallService.GetAllAsync();

            return Ok(_mapster.Map<List<CinemaHallResponse>>(cinemaHalls));
        }

        [AllowAnonymous]
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<CinemaHallResponse>> GetById(Guid id)
        {
            CinemaHall? cinemaHall = await _cinemaHallService.FindByIdAsync(id);

            if (cinemaHall == null)
            {
                return NotFound("Cinema hall not found");
            }

            CinemaHallResponse? cinemaHallResponse = _mapster.Map<CinemaHall, CinemaHallResponse>(cinemaHall);

            return Ok(cinemaHallResponse);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<CinemaHallResponse>> Create(CinemaHallDto cinemaHallDto)
        {
            CinemaHall newCinemaHall = await _cinemaHallService.Insert(_mapster.Map<CinemaHall>(cinemaHallDto));

            return CreatedAtAction(nameof(GetById), new { id = newCinemaHall.Id }, _mapster.Map<CinemaHallResponse>(newCinemaHall));
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<CinemaHallResponse>> Update(Guid id, CinemaHallDto cinemaHallDto)
        {
            CinemaHall? existingCinemaHall = await _cinemaHallService.FindByIdAsync(id);

            if (existingCinemaHall == null)
            {
                return NotFound("Cinema hall not found");
            }

            CinemaHall cinemaHall = _mapster.Map(cinemaHallDto, existingCinemaHall);

            return Ok(await _cinemaHallService.Update(cinemaHall));
        }

        [Authorize(Roles = "Admin")]
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
