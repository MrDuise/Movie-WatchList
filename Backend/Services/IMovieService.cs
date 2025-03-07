namespace Movie_WatchList.Services;

using System.Threading.Tasks;
using Movie_WatchList.Models;

public interface IMovieService
{
    Task<PaginatedMovieResponse> GetPopularMoviesAsync(int page);
    Task<PaginatedMovieResponse> SearchMoviesAsync(string query, int page);
}
