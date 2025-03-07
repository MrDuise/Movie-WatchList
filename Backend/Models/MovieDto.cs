namespace Movie_WatchList.Models;

using System;
using System.Text.Json.Serialization;



public class MovieDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("overview")]
    public string Overview { get; set; }

    [JsonPropertyName("release_date")]
    public string ReleaseDate { get; set; }

    [JsonPropertyName("vote_average")]
    public double Rating { get; set; }

    [JsonPropertyName("poster_path")]
    public string PosterPath { get; set; }
}

