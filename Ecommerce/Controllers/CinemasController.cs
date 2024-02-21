using Ecommerce.Data;
using Ecommerce.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Controllers
{
    public class CinemasController : Controller
    {
        private readonly AppDbContext _Context;

        public CinemasController(AppDbContext context)
        {
            _Context = context;
        }
        public IActionResult Index()
        {
            var allCinemas = _Context.Cinemas.ToList();

            return View(allCinemas);
        }
        //get:Actors/create
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,CinmaLogo,Name,Discription")] Cinema cinema)
        {
            if (ModelState.IsValid)
            {
                _Context.Add(cinema);
                await _Context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cinema);
        }
        // GET: Cinmas1/Details/1
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _Context.Cinemas == null)
            {
                return View("NotFound");
            }

            var cinema = await _Context.Cinemas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cinema == null)
            {
                return View("NotFound");
            }

            return View(cinema);
        }
        // GET: Cinemas1/Edit/1
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _Context.Cinemas == null)
            {
                return View("NotFound");
            }

            var cinema = await _Context.Cinemas.FindAsync(id);
            if (cinema == null)
            {
                return View("NotFound");
            }
            return View(cinema);
        }

        // POST: Cinemas1/Edit/1
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CinmaLogo,Name,Discription")] Cinema cinema)
        {
            if (id != cinema.Id)
            {
                return View("NotFound");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _Context.Update(cinema);
                    await _Context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CinemaExists(cinema.Id))
                    {
                        return View("NotFound");
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(cinema);
        }
        // GET: Cinemas1/Delete/1
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _Context.Cinemas == null)
            {
                return View("NotFound");
            }

            var cinema = await _Context.Cinemas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cinema == null)
            {
                return View("NotFound");
            }

            return View(cinema);
        }

        // POST: Cinemas1/Delete/1
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_Context.Cinemas == null)
            {
                return Problem("Entity set 'AppDbContext.Cinemas'  is null.");
            }
            var cinema = await _Context.Cinemas.FindAsync(id);
            if (cinema != null)
            {
                _Context.Cinemas.Remove(cinema);
            }

            await _Context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CinemaExists(int id)
        {
            return (_Context.Cinemas?.Any(e => e.Id == id)).GetValueOrDefault();
        }

    }
}
