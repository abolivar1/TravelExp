using System;
using System.Collections.Generic;
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

namespace TravelExp.Web.Controllers.API
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly DataContext _context;

        public CitiesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/City
        [HttpGet]
        public IEnumerable<City> GetCities()
        {
            return _context.Cities;
        }

        // GET: api/City/5
        /*[HttpGet("{id}")]
        public async Task<IActionResult> GetCityResponse([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cityResponse = await _context.CityResponse.FindAsync(id);

            if (cityResponse == null)
            {
                return NotFound();
            }

            return Ok(cityResponse);
        }*/
    }
}