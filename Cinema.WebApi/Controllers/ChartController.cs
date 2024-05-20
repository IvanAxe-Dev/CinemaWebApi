using Cinema.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;
namespace Cinema.WebApi.Controllers;


[Route("api/[controller]")]
[ApiController]
public class ChartsController : BaseController
{
    private readonly IMovieService _movieService;

    private record CountByYearResponseItem(string Year, int Count);
    public ChartsController(IMovieService movieService)
    {
        _movieService = movieService;
    }

    [HttpGet("countByYear")]
    public async Task<JsonResult> GetCountByYearAsync()
    {
        var responseItems = await _movieService.GetAllMoviesWithCategories();
        var result =  responseItems
            .GroupBy(movie => movie.ReleaseDate!.Value.Year)
            .Select(group => new CountByYearResponseItem(
                group.Key.ToString(), group.Count())).ToList();
        return new JsonResult(result);
    }
}