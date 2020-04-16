using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelExp.Web.Data;
using TravelExp.Web.Data.Entities;

namespace TravelExp.Web.Controllers.API
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseTypesController : ControllerBase
    {
        private readonly DataContext _context;

        public ExpenseTypesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/ExpenseTypes
        [HttpGet]
        public IEnumerable<ExpenseType> GetExpenseTypes()
        {
            return _context.ExpenseTypes;
        }

        /* GET: api/ExpenseTypes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetExpenseType([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var expenseType = await _context.ExpenseTypes.FindAsync(id);

            if (expenseType == null)
            {
                return NotFound();
            }

            return Ok(expenseType);
        }*/

    }
}