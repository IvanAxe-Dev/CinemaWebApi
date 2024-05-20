
using Cinema.Core.DTO;
using Cinema.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;
namespace Cinema.WebApi.Controllers;


[Route("api/[controller]")]
[ApiController]
public class ChartsController : BaseController
{
    private readonly IMovieService _movieService;

    private readonly ISessionService _sessionService;

    private record CountByYearResponseItem(string Year, int Count);
    
    public ChartsController(IMovieService movieService, ISessionService sessionService)
    {
        _movieService = movieService;
        _sessionService = sessionService;
    }

    [HttpGet("countByYear")]
    public async Task<JsonResult> GetCountByYearAsync()
    {
        List<MovieResponse> responseItems = await _movieService.GetAllMoviesWithCategories();
        var result =  responseItems
            .GroupBy(movie => movie.ReleaseDate!.Value.Year)
            .Select(group => new CountByYearResponseItem(
                group.Key.ToString(), group.Count())).ToList();
        return new JsonResult(result);
    }

    [HttpGet("seatsByYear")]
    public async Task<JsonResult> GetTotalPriceByCategory()
    {

        var sessions = await _sessionService.GetAllAsync();
        var result = sessions.Select(el => new
        {
            TicketsSold = el.Tickets.Count - el.AvailableTickets,
            el.Date.Year,
            el.Movie.Title,
            el.Price
        }).GroupBy(el => el.TicketsSold).ToList();
        return new JsonResult(result);
    }
}