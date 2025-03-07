namespace Movie_WatchList.Services;
using Movie_WatchList.Models;
public interface IWatchlistService
{
    Task<List<WatchlistMovie>> GetWatchlist();
    Task<List<WatchlistMovie>> AddMovieAsync(List<MovieDto> dtoList);
    Task<bool> RemoveFromWatchlist(int id);
    Task<WatchlistMovie> MarkAsWatched(int id);
}
