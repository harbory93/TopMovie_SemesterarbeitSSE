using System.ComponentModel.DataAnnotations;

namespace TopMovie_SemesterarbeitSSE.Models
{
    public class Cinema
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;

        // Relationships
        public ICollection<Theater>? Theaters { get; set; }
    }
}
