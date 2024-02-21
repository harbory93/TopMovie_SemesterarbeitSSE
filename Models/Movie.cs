using System.ComponentModel.DataAnnotations;
using TopMovie_SemesterarbeitSSE.Enums;

namespace TopMovie_SemesterarbeitSSE.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public EMovieGenres Genre { get; set; }
        public string Director { get; set; }
        public string Cast { get; set; }
        public int Duration { get; set; }
        public string ImagePath { get; set; }

        // Relationship
        public ICollection<Schedule> Schedules { get; set; }

    }
}