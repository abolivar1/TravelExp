using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelExp.Common.Models;
using TravelExp.Web.Data;
using TravelExp.Web.Data.Entities;
using TravelExp.Web.Helpers;
using TravelExp.Web.Resources;

namespace TravelExp.Web.Controllers.API
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("api/[controller]")]
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

        [HttpPost]
        public async Task<IActionResult> PostTrip([FromBody] TripRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new Response
                {
                    IsSuccess = false,
                    Message = "Bad request",
                    Result = ModelState
                });
            }

            CultureInfo cultureInfo = new CultureInfo(request.CultureInfo);
            Resource.Culture = cultureInfo;

            var trip = new Trip
            {
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Employee = _context.Users.Find(request.EmployeeId),
                City = _context.Cities.Find(request.CityId),
                TotalAmount = 0,
                TripDetails = new List<TripDetail> { }
            };

            await _context.Trips.AddAsync(trip);
            await _context.SaveChangesAsync();
            return Ok(new Response
            {
                IsSuccess = true,
                Message = "The trip has been created"
            });
        }

        [HttpPost]
        [Route("AddTripDetail")]
        public async Task<IActionResult> AddTripDetail([FromBody] TripDetailRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new Response
                {
                    IsSuccess = false,
                    Message = "Bad request",
                    Result = ModelState
                });
            }

            CultureInfo cultureInfo = new CultureInfo(request.CultureInfo);
            Resource.Culture = cultureInfo;

            var tripdetail = new TripDetail
            {
                Date = request.Date,
                Amount = request.Amount,
                Trip = _context.Trips.Find(request.TripId),
                ExpenseType = _context.ExpenseTypes.Find(request.ExpenseTypeId),
                Description = request.Description,
                PicturePath = request.PicturePath
            };

            await _context.TripDetails.AddAsync(tripdetail);
            Trip trip = _context.Trips.Find(request.TripId);
            trip.TotalAmount += request.Amount;
            _context.Trips.Update(trip);
            await _context.SaveChangesAsync();
            return Ok(new Response
            {
                IsSuccess = true,
                Message = "The trip detail has been created"
            });
        }
    }
}