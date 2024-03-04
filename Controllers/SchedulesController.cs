using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TopMovie_SemesterarbeitSSE.Data;
using TopMovie_SemesterarbeitSSE.Enums;
using TopMovie_SemesterarbeitSSE.Models;

namespace TopMovie_SemesterarbeitSSE.Controllers
{
    public class SchedulesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public SchedulesController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
            
        }

        // GET: Schedules
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Schedule.Include(s => s.Movie).Include(s => s.Theater);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Schedules/SearchForm
        public IActionResult SearchForm()
        {
            return View();
        }

        // POST: Schedules/ShowSearch
        public async Task<IActionResult> ShowSearch(string SearchPhrase)
        {
            var applicationDbContext = _context.Schedule
                .Include(j => j.Movie)
                .Include(j => j.Theater)
                .Where(j => (j.Movie != null && j.Movie.Title.Contains(SearchPhrase)) ||
                            (j.Movie != null && j.Movie.Description.Contains(SearchPhrase)) ||
                            (j.Movie != null && j.Movie.Director.Contains(SearchPhrase)) ||
                            (j.Movie != null && j.Movie.Cast.Contains(SearchPhrase)) ||
                            (j.Theater != null && j.Theater.Name.Contains(SearchPhrase)));

            return View("Index", await applicationDbContext.ToListAsync());
        }

        // GET: Schedules/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schedule = await _context.Schedule
                .Include(s => s.Movie)
                .Include(s => s.Theater)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (schedule == null)
            {
                return NotFound();
            }

            return View(schedule);
        }

        // GET: Schedules/Create
        public IActionResult Create()
        {
            ViewData["MovieId"] = new SelectList(_context.Movie, "Id", "Title");
            ViewData["TheaterId"] = new SelectList(_context.Theater, "Id", "Name");
            ViewBag.ScheduleTimesSelectList = EnumHelper.GetSelectListForEnum<EScheduleTimes>();

            return View();    
        }

        // POST: Schedules/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Time,Date,MovieId,TheaterId")] Schedule schedule)
        {
            if (ModelState.IsValid)
            {
                _context.Add(schedule);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MovieId"] = new SelectList(_context.Movie, "Id", "Title", schedule.MovieId);
            ViewData["TheaterId"] = new SelectList(_context.Theater, "Id", "Name", schedule.TheaterId);
            return View(schedule);
        }

        // GET: Schedules/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schedule = await _context.Schedule.FindAsync(id);
            if (schedule == null)
            {
                return NotFound();
            }
            ViewData["MovieId"] = new SelectList(_context.Movie, "Id", "Title", schedule.MovieId);
            ViewData["TheaterId"] = new SelectList(_context.Theater, "Id", "Name", schedule.TheaterId);
            return View(schedule);
        }

        // POST: Schedules/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Time,Date,MovieId,TheaterId")] Schedule schedule)
        {
            if (id != schedule.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(schedule);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ScheduleExists(schedule.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MovieId"] = new SelectList(_context.Movie, "Id", "Title", schedule.MovieId);
            ViewData["TheaterId"] = new SelectList(_context.Theater, "Id", "Name", schedule.TheaterId);
            return View(schedule);
        }

        // GET: Schedules/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schedule = await _context.Schedule
                .Include(s => s.Movie)
                .Include(s => s.Theater)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (schedule == null)
            {
                return NotFound();
            }

            return View(schedule);
        }

        // POST: Schedules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var schedule = await _context.Schedule.FindAsync(id);
            if (schedule != null)
            {
                _context.Schedule.Remove(schedule);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // POST: Schedules/SelectSeats
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SelectSeats(int scheduleId, int numberOfSeats)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Challenge();
            }

            var schedule = await _context.Schedule.Include(s => s.Theater).FirstOrDefaultAsync(s => s.Id == scheduleId);
            if (schedule == null)
            {
                return NotFound();
            }

            var availableSeats = schedule.Theater.Capacity - schedule.SeatsBooked;
            if (availableSeats >= numberOfSeats)
            {
                var booking = new Booking {
                    UserId = user.Id,
                    ScheduleId = scheduleId,
                    NumberOfSeats = numberOfSeats
                };
                _context.Booking.Add(booking);

                schedule.SeatsBooked += numberOfSeats;
                await _context.SaveChangesAsync();

                TempData["MessageKey"] = "BookingSuccess";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["MessageKey"] = "NotEnoughSeats";
                return RedirectToAction("Index");
            }
        }


        private bool ScheduleExists(int id)
        {
            return _context.Schedule.Any(e => e.Id == id);
        }
    }
}
