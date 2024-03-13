using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TopMovie_SemesterarbeitSSE.Models;

namespace TopMovie_SemesterarbeitSSE.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<TopMovie_SemesterarbeitSSE.Models.Movie> Movie { get; set; } = default!;
        public DbSet<TopMovie_SemesterarbeitSSE.Models.Cinema> Cinema { get; set; } = default!;
        public DbSet<TopMovie_SemesterarbeitSSE.Models.Theater> Theater { get; set; } = default!;
        public DbSet<TopMovie_SemesterarbeitSSE.Models.Schedule> Schedule { get; set; } = default!;
        public DbSet<TopMovie_SemesterarbeitSSE.Models.Booking> Booking { get; set; } = default!;
    }
}
