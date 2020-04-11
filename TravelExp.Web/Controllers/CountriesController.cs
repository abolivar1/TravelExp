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
using TravelExp.Web.Helpers;
using TravelExp.Web.Models;

namespace TravelExp.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CountriesController : Controller
    {
        private readonly DataContext _context;
        private readonly IConverterHelper _converterHelper;

        public CountriesController(
            DataContext context,
            IConverterHelper converterHelper)
        {
            _context = context;
            _converterHelper = converterHelper;
        }

        // GET: Countries
        public async Task<IActionResult> Index()
        {
            return View(await _context.Countries.Include(c => c.Cities).
                ToListAsync());
        }

        // GET: Countries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = await _context.Countries.Include(c => c.Cities)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Country country)
        {
            if (ModelState.IsValid)
            {
                _context.Add(country);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(country);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = await _context.Countries.FindAsync(id);
            if (country == null)
            {
                return NotFound();
            }
            return View(country);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Country country)
        {
            if (id != country.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(country);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CountryExists(country.Id))
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
            return View(country);
        }

        // GET: Countries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = await _context.Countries
                .Include(p => p.Cities)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (country == null)
            {
                return NotFound();
            }
            if (country.Cities.Count > 0)
            {
                TempData["DeleteFail"] = "You can not delete the country " + country.Name + " because it has associated cities";
                return RedirectToAction(nameof(Index));
            }
            _context.Countries.Remove(country);
            await _context.SaveChangesAsync();
            TempData["DeleteOK"] = "The country " + country.Name + " has been deleted";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> AddCity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Country country = await _context.Countries.FindAsync(id);
            if (country == null)
            {
                return NotFound();
            }

            CityViewModel model = new CityViewModel
            {
                Country = country,
                CountryId = country.Id
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCity(CityViewModel model)
        {
            if (ModelState.IsValid)
            {
                City city = await _converterHelper.ToCityAsync(model, true);
                _context.Add(city);
                await _context.SaveChangesAsync();
                return RedirectToAction($"{nameof(Details)}/{model.CountryId}");
            }
            return View(model);
        }

        public async Task<IActionResult> EditCity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var city = await _context.Cities.Include(c => c.Country).FirstOrDefaultAsync(c => c.Id == id);
            if (city == null)
            {
                return NotFound();
            }

            CityViewModel model = new CityViewModel
            {
                Country = city.Country,
                CountryId = city.Country.Id,
                CityId = city.Id,
                Name = city.Name
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCity(CityViewModel model)
        {
            if (ModelState.IsValid)
            {
                City city = await _converterHelper.ToCityAsync(model, false);
                _context.Update(city);
                await _context.SaveChangesAsync();
                return RedirectToAction($"{nameof(Details)}/{model.CountryId}");
            }

            return View(model);
        }

        public async Task<IActionResult> DeleteCity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var city = await _context.Cities
                .Include(c => c.Country)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (city == null)
            {
                return NotFound();
            }
            _context.Cities.Remove(city);
            await _context.SaveChangesAsync();
            TempData["DeleteOK"] = "The city " + city.Name + " has been deleted";
            return RedirectToAction($"{nameof(Details)}/{city.Country.Id}");
        }
        private bool CountryExists(int id)
        {
            return _context.Countries.Any(e => e.Id == id);
        }
    }
}
