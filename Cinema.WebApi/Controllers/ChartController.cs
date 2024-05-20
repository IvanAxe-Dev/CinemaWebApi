using Cinema.Core.Domain.Entities;
using Cinema.Core.DTO;
using Cinema.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;
namespace Cinema.WebApi.Controllers;


[Route("api/[controller]")]
[ApiController]
public class ChartsController : BaseController
{
    private readonly IMovieService _movieService;
    private readonly ITicketService _ticketService;

    private record CountByYearResponseItem(string Year, int Count);
    
    private record SeatsByYearResponseItem(string Tickets, int Seats);
    public ChartsController(IMovieService movieService, ITicketService ticketService)
    {
        _movieService = movieService;
        _ticketService = ticketService;
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
        List<Ticket> tickets = await _ticketService.GetAllAsync();
        var result = tickets.Select(element => new
        {
            TicketsSold = element.Session.Tickets.Count - element.Session.AvailableTickets,
            element.Session.Date.Year,
        }).GroupBy(element => element.Year).ToList();
        return new JsonResult(result);
    }
}