using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelExp.Web.Data;
using TravelExp.Web.Data.Entities;
using TravelExp.Web.Helpers;

namespace TravelExp.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripsController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConverterHelper _converterHelper;

        public TripsController(
            DataContext context,
            IConverterHelper converterHelper)
        {
            _context = context;
            _converterHelper = converterHelper;
        }

        [HttpGet]
        public async Task<IActionResult> GetTrips()
        {
            List<Trip> trips = await _context.Trips
                .Include(t => t.City)
                .ThenInclude(c => c.Country)
                .Include(t => t.Employee)
                .Include(t => t.TripDetails)
                .ThenInclude(td => td.ExpenseType)
                .ToListAsync();
            return Ok(_converterHelper.ToTripResponse(trips));
        }

    }
}