using System.ComponentModel.DataAnnotations;

namespace TopMovie_SemesterarbeitSSE.Models
{
    public class Theater
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }

        // Relationships
        public int CinemaId { get; set; }
        public Cinema? Cinema { get; set; }

        public ICollection<Schedule> Schedules { get; set; }
    }
}
