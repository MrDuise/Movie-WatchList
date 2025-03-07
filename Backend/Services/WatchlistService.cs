namespace Movie_WatchList.Services;

using Movie_WatchList.Data;
using Movie_WatchList.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;

public class WatchlistService : IWatchlistService
{
    public WatchlistMovie ConvertToWatchlistMovie(MovieDto dto)
    {
        return new WatchlistMovie
        {
            Id = dto.Id,
            Title = dto.Title,
            Overview = dto.Overview,
            PosterPath = dto.PosterPath,
            Rating = dto.Rating,
            Watched = false,  // Default to not watched
            AddedDate = DateTime.UtcNow
        };
    }

    private readonly MovieWatchlistDbContext _context;

    public WatchlistService(MovieWatchlistDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<List<WatchlistMovie>> AddMovieAsync(List<MovieDto> dtoList)
    {
        if (dtoList == null)
        {
            throw new ArgumentNullException(nameof(dtoList), "Movie DTO cannot be null");
        }

        // Convert the list of MovieDto to WatchlistMovie
        var moviesToAdd = dtoList.Select(dto => ConvertToWatchlistMovie(dto)).ToList();

        //Get the IDs of the movies that are already in the database
        var existingMovieIds = await _context.WatchlistMovies
                                        .Where(m => moviesToAdd.Select(movie => movie.Id).Contains(m.Id))
                                        .Select(m => m.Id)
                                        .ToListAsync();

        // Check if the movie already exists
        // Filter out movies that are already in the watchlist
        var newMovies = moviesToAdd.Where(m => !existingMovieIds.Contains(m.Id)).ToList();

        // If there are new movies, add them to the database
        if (newMovies.Any())
        {
            await _context.WatchlistMovies.AddRangeAsync(newMovies);
            await _context.SaveChangesAsync();
        } else
        {
            throw new InvalidOperationException("A selected movie is already in the watchlist.");
        }

         return newMovies;
     
    }

    public async Task<List<WatchlistMovie>> GetWatchlist()
    {
        var watchList = await _context.WatchlistMovies.ToListAsync();

        return watchList ?? new List<WatchlistMovie>();
    }

    public async Task<WatchlistMovie> MarkAsWatched(int id)
    {
        var movie = await _context.WatchlistMovies
                            .FirstOrDefaultAsync(m => m.Id == id);

        // If the movie is found, update its 'Watched' column to true
        if (movie != null)
        {
            movie.Watched = true;
            await _context.SaveChangesAsync();
        }

        return movie;
    }

    public async Task<bool> RemoveFromWatchlist(int id)
    {
        var movie = await _context.WatchlistMovies
                             .FirstOrDefaultAsync(m => m.Id == id);

        // If the movie is found, remove it
        if (movie != null)
        {
            _context.WatchlistMovies.Remove(movie);
            await _context.SaveChangesAsync();
            return true;
        }

        // Return false if the movie was not found
        return false;
    }
}

