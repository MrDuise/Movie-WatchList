using Microsoft.AspNetCore.Mvc;
using System;
using Movie_WatchList.Models;
using Movie_WatchList.Services;
using System.Threading.Tasks;

[ApiController]
[Route("[controller]")]
public class WatchlistController : ControllerBase
{
    private readonly ILogger<WatchlistController> _logger;
    private readonly IWatchlistService _watchlistService;

    public WatchlistController(ILogger<WatchlistController> logger, IWatchlistService service)
    {
        _logger = logger;
        _watchlistService = service;
    }

    // GET /api/watchlist - Get the user's watchlist
    [HttpGet]
    public async Task<IActionResult> GetWatchlist()
    {
        try
        {
            var watchList = await _watchlistService.GetWatchlist();
            return Ok(watchList);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }

    }

    // POST /api/watchlist/add - Add movies to the watchlist
    [HttpPost("add")]
    public async Task<IActionResult> AddToWatchlist([FromBody] List<MovieDto> movieDto)
    {
        try
        {
            var addedMovie = await _watchlistService.AddMovieAsync(movieDto);
            return Ok(addedMovie);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
       
    }

    // DELETE /api/watchlist/remove/{id} - Remove movie from watchlist
    [HttpDelete("remove/{id}")]
    public async Task<IActionResult> RemoveFromWatchlist(int id)
    {
        try
        {
           var removed =  await _watchlistService.RemoveFromWatchlist(id);
            return Ok(removed);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    // PATCH /api/watchlist/watched/{id} - Mark movie as watched
    [HttpPatch("watched/{id}")]
    public async Task<IActionResult> MarkAsWatched(int id)
    {
        try
        {
            var updatedMovie = await _watchlistService.MarkAsWatched(id);
            return Ok(updatedMovie.Watched);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

}
