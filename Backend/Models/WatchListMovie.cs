namespace Movie_WatchList.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class WatchlistMovie
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)] // Disable auto-increment
    public int Id { get; set; }  // Primary Key

    [Required]
    [MaxLength(255)]
    public string Title { get; set; }

    [Required]
    public string Overview { get; set; }

    public string PosterPath { get; set; }

    [Column(TypeName = "decimal(3,1)")]
    public double Rating { get; set; }

    // Watchlist-specific fields
    public bool Watched { get; set; }
    public DateTime AddedDate { get; set; }
}
