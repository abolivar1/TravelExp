using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelExp.Web.Data;

namespace TravelExp.Web.Controllers
{
    public class TripsController : Controller
    {
        private readonly DataContext _context;

        public TripsController(DataContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(
                await _context.Users.Include(u => u.Trips)
                .ToListAsync());
        }

        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Users.Include(u => u.Trips)
                .ThenInclude(t => t.TripDetails)
                .Include(u => u.Trips)
                .ThenInclude(t => t.City)
                .ThenInclude(c => c.Country)
                .FirstOrDefaultAsync(u => u.Id.Equals(id));
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        public async Task<IActionResult> TripDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trip = await _context.Trips.Include(u => u.TripDetails)
                .ThenInclude(t => t.ExpenseType)
                .Include(u => u.City)
                .ThenInclude(c => c.Country)
                .Include(t => t.Employee)
                .FirstOrDefaultAsync(u => u.Id == id);
            if (trip == null)
            {
                return NotFound();
            }

            return View(trip);
        }
    }
}