namespace Movie_WatchList.Services;

using System.Threading.Tasks;
using Movie_WatchList.Models;
using RestSharp;

public interface IMovieService
{
    Task<RestResponse> GetPopularMoviesAsync(int page);
    Task<RestResponse> SearchMoviesAsync(string query, int page);
}
