using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace TopMovie_SemesterarbeitSSE.Models
{
    public class Booking
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public int ScheduleId { get; set; }
        public int NumberOfSeats { get; set; }

        // Navigation property
        public Schedule? Schedule { get; set; }
        public IdentityUser? User { get; set; }
    }
}
