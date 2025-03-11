using Moq;
using Xunit;
using Movie_WatchList.Controllers;
using Movie_WatchList.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Movie_WatchList.Models;
using Newtonsoft.Json;
using RestSharp;
using System.Text.Json;

public class MovieControllerTests
{
    private readonly Mock<IMovieService> _mockMovieService;
    private readonly MovieController _controller;

    public MovieControllerTests()
    {
        _mockMovieService = new Mock<IMovieService>();
        _controller = new MovieController(null, _mockMovieService.Object);
    }

    [Fact]
    public async Task GetPopularMovies_ReturnsOkResult_WhenMoviesAreFound()
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

        _mockMovieService.Setup(service => service.GetPopularMoviesAsync(1)).ReturnsAsync(mockResponse);

        // Act
        var result = await _controller.GetPopularMovies(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<PaginatedMovieResponse>(okResult.Value);
        Assert.NotEmpty(returnValue.Movies);
        Assert.True(returnValue.TotalPages == 10);
    }

    [Fact]
    public async Task SearchMovies_ReturnsOkResult_WhenMoviesAreFound()
    {
        // Arrange
        var query = "test";
        var page = 1;
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
        _mockMovieService.Setup(service => service.SearchMoviesAsync(query, page)).ReturnsAsync(mockResponse);

        // Act
        var result = await _controller.SSearchMovies(query, page);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<PaginatedMovieResponse>(okResult.Value);
        Assert.NotEmpty(returnValue.Movies);
    }
}

