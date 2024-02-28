using TopMovie_SemesterarbeitSSE.Data;
using TopMovie_SemesterarbeitSSE.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;


namespace TopMovie_SemesterarbeitSSE.Areas.Identity.Pages.Account.Manage
{
    public class BookingsModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context;

        public BookingsModel(
            UserManager<IdentityUser> userManager,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public IList<Booking> Bookings { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            // Adjust the query to use UserIdentityId
            Bookings = await _context.Booking
                .Include(b => b.Schedule)
                    .ThenInclude(s => s.Movie)
                .Include(b => b.Schedule)
                    .ThenInclude(s => s.Theater)
                .Where(b => b.UserId == user.Id) // Adjust this line
                .ToListAsync();

            return Page();
        }
    }
}
