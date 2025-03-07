using Microsoft.AspNetCore.Mvc;
using Movie_WatchList.Services;
using System;

namespace Movie_WatchList.Controllers;


[ApiController]
[Route("[controller]")]

public class MovieController: ControllerBase
{
    private readonly ILogger<MovieController> _logger;
    private readonly IMovieService _movieService;

    public MovieController(ILogger<MovieController> logger, IMovieService movieService)
    {
        _logger = logger;
        _movieService = movieService;
    }

    // GET /api/movies/popular - Fetch popular movies (cached in Redis)
    [HttpGet("popular")]
    public async Task<IActionResult> GetPopularMovies([FromQuery] int page = 1)
    {
        var movies = await _movieService.GetPopularMoviesAsync(page);
        return Ok(movies);
    }

    // GET /api/movies/search?query= - Search movies by title
    [HttpGet("search")]
    public async Task<IActionResult> SSearchMovies([FromQuery] string query, [FromQuery] int page = 1)
    {
        var movies = await _movieService.SearchMoviesAsync(query, page);
        return Ok(movies);
    }


}
