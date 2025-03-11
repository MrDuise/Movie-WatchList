using Moq;
using Xunit;
using RestSharp;
using Movie_WatchList.Services;
using Movie_WatchList.Models;
using System.Threading.Tasks;
using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using System;

public class MovieServiceTests
{
    private readonly Mock<IRestClient> _mockClient;
    private readonly MovieService _movieService;

    public MovieServiceTests()
    {
       
        // Set a fake token for testing
        Environment.SetEnvironmentVariable("authToken", "Bearer fake-test-token");
        _mockClient = new Mock<IRestClient>();
        _movieService = new MovieService();
    }


    [Fact]
    public async Task GetPopularMoviesAsync_ReturnsMovies_WhenApiCallIsSuccessful()
    {
        // Arrange
        var mockResponseObject = new PaginatedMovieResponse()
        {
            PageNumber = 1,
            TotalPages = 10,
            Movies = new List<MovieDto>()
    {
        new MovieDto
        {
            Id = 1,
            Title = "title",
            Overview = "Overview",
            ReleaseDate = "Date",
            Rating = 1.0,
            PosterPath = "path"
        }
    }
        };

        // Serialize the mockResponseObject to JSON
        var jsonMockResponse = System.Text.Json.JsonSerializer.Serialize(mockResponseObject, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        // Create a RestResponse and set the content to the JSON response
        var mockResponse = new RestResponse
        {
            Content = jsonMockResponse,
            StatusCode = System.Net.HttpStatusCode.OK, // Simulate a successful response
            ResponseStatus = ResponseStatus.Completed, // Ensure the response is marked as completed
            IsSuccessStatusCode = true // For newer RestSharp versions (if applicable)
        };

        // Create mock RestClient and setup the GetAsync method
        _mockClient.Setup(client => client.ExecuteAsync(It.IsAny<RestRequest>(), It.IsAny<CancellationToken>()))
        .ReturnsAsync(mockResponse);

        // Act
        var result = await _movieService.GetPopularMoviesAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<string>(result.Content);
        
        //Assert.Single(result.Movies); // Ensure there's one movie in the result
        //Assert.Equal("Movie 1", result.Movies[0].Title); // Ensure the title matches
        //Assert.Equal(1, result.Movies[0].Id); // Check the ID of the movie
        //Assert.Equal("Overview", result.Movies[0].Overview); // Check the Overview field
        //Assert.Equal("2025-01-01", result.Movies[0].ReleaseDate); // Check the ReleaseDate
        //Assert.Equal(8.5, result.Movies[0].Rating); // Check the Rating
        //Assert.Equal("/path/to/poster", result.Movies[0].PosterPath); // Check the PosterPath
    }


    [Fact]
    public async Task GetPopularMoviesAsync_ReturnsEmptyResponse_WhenApiCallFails()
    {
        // Arrange
        var mockResponse = new RestResponse
        {
            Content = null,
            StatusCode = System.Net.HttpStatusCode.InternalServerError, // Simulate a successful response
            ResponseStatus = ResponseStatus.Error, // Ensure the response is marked as completed
            IsSuccessStatusCode = false // For newer RestSharp versions (if applicable)
        };

        _mockClient.Setup(client => client.ExecuteAsync(It.IsAny<RestRequest>(), It.IsAny<CancellationToken>()))
        .ReturnsAsync(mockResponse);

        // Act
        var result = await _movieService.GetPopularMoviesAsync();

        // Assert
        Assert.NotNull(result);
        Assert.False(result.IsSuccessful);
    }
}

