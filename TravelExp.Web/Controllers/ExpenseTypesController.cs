using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TravelExp.Web.Data;
using TravelExp.Web.Data.Entities;

namespace TravelExp.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ExpenseTypesController : Controller
    {
        private readonly DataContext _context;

        public ExpenseTypesController(DataContext context)
        {
            _context = context;
        }

        // GET: ExpenseTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.ExpenseTypes.ToListAsync());
        }

        // GET: ExpenseTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expenseType = await _context.ExpenseTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (expenseType == null)
            {
                return NotFound();
            }

            return View(expenseType);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ExpenseType expenseType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(expenseType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(expenseType);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expenseType = await _context.ExpenseTypes.FindAsync(id);
            if (expenseType == null)
            {
                return NotFound();
            }
            return View(expenseType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ExpenseType expenseType)
        {
            if (id != expenseType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(expenseType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExpenseTypeExists(expenseType.Id))
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
            return View(expenseType);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expenseType = await _context.ExpenseTypes
                .Include(et => et.TripDetails)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (expenseType == null)
            {
                return NotFound();
            }

            if (expenseType.TripDetails.Count > 0)
            {
                TempData["DeleteFail"] = "You can not delete the expense type " + expenseType.Name + " because it has associated trips details";
                return RedirectToAction(nameof(Index));
            }
            _context.ExpenseTypes.Remove(expenseType);
            await _context.SaveChangesAsync();
            TempData["DeleteOK"] = "The expense type " + expenseType.Name + " has been deleted";
            return RedirectToAction(nameof(Index));
        }

        private bool ExpenseTypeExists(int id)
        {
            return _context.ExpenseTypes.Any(e => e.Id == id);
        }
    }
}
