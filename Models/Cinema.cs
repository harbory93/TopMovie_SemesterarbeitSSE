using System.ComponentModel.DataAnnotations;

namespace TopMovie_SemesterarbeitSSE.Models
{
    public class Cinema
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string ImagePath { get; set; }

        // Relationships
        public ICollection<Theater> Theaters { get; set; }
    }
}
