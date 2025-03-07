namespace Movie_WatchList.Models;

using System.Collections.Generic;
using System.Text.Json.Serialization;


public class PaginatedMovieResponse
{
    [JsonPropertyName("page")]
    public int PageNumber { get; set; }

    [JsonPropertyName("total_pages")]
    public int TotalPages { get; set; }

    [JsonPropertyName("results")]
    public List<MovieDto> Movies { get; set; } = new List<MovieDto>();


}

