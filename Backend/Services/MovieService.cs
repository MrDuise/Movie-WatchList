namespace Movie_WatchList.Services;

using System;
using RestSharp;
using System.Collections.Generic;
using System.Text.Json;
using Movie_WatchList.Models;
using DotNetEnv;

public class MovieService: IMovieService
{
    private readonly RestClient _client;
    private readonly string authToken;

    public MovieService()
    {
        _client = new RestClient("https://api.themoviedb.org/3");
        authToken = Environment.GetEnvironmentVariable("authToken");
    }

    public async Task<PaginatedMovieResponse> GetPopularMoviesAsync(int page = 1)
    {
        var request = new RestRequest($"/trending/movie/week?language=en-US&page={page}");
        request.AddHeader("accept", "application/json");
        request.AddHeader("Authorization", authToken);
        
        //send the request
        var response = await _client.GetAsync(request);
        //if request is empty, just return an empty response
        if (!response.IsSuccessful) return new PaginatedMovieResponse();

        //serialize the response
        var result = JsonSerializer.Deserialize<PaginatedMovieResponse>(response.Content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        return result ?? new PaginatedMovieResponse();
    }

    public async Task<PaginatedMovieResponse> SearchMoviesAsync(string query, int page = 1)
    {
        var request = new RestRequest($"search/movie?query={query}&include_adult=false&language=en-US&page={page}");
        request.AddHeader("Authorization", authToken);

        var response = await _client.ExecuteAsync(request);
        if (!response.IsSuccessful) return new PaginatedMovieResponse();

        // Deserialize JSON directly into PaginatedMovieResponse
        var result = JsonSerializer.Deserialize<PaginatedMovieResponse>(response.Content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        return result ?? new PaginatedMovieResponse();
    }
}
