using Movie_WatchList.Models;
using Microsoft.EntityFrameworkCore;


namespace Movie_WatchList.Data;

public class MovieWatchlistDbContext : DbContext
{
    public MovieWatchlistDbContext(DbContextOptions<MovieWatchlistDbContext> options) : base(options) { }

    public DbSet<WatchlistMovie> WatchlistMovies { get; set; }
}
