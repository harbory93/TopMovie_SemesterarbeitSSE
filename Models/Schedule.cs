using System.ComponentModel.DataAnnotations;
using TopMovie_SemesterarbeitSSE.Enums;

namespace TopMovie_SemesterarbeitSSE.Models
{
    public class Schedule
    {
        [Key]
        public int Id { get; set; }
        public EScheduleTimes Time { get; set; }
        public DateOnly Date { get; set; }

        // Relationships
        public int MovieId { get; set; }
        public Movie? Movie { get; set; }

        public int TheaterId { get; set; }
        public Theater? Theater { get; set; }

    }
}
