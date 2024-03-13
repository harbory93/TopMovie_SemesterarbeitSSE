using System.ComponentModel.DataAnnotations;
using TopMovie_SemesterarbeitSSE.Enums;

namespace TopMovie_SemesterarbeitSSE.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public EMovieGenres Genre { get; set; }
        public string Director { get; set; } = string.Empty;
        public string Cast { get; set; } = string.Empty;
        public int Duration { get; set; }
        public string ImagePath { get; set; } = string.Empty;

        // Relationship
        public ICollection<Schedule>? Schedules { get; set; }

    }
}