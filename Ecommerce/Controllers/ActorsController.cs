using Ecommerce.Data;
using Ecommerce.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Controllers
{
    public class ActorsController : Controller
    {
        private readonly AppDbContext _Context;

        public ActorsController(AppDbContext context)
        {
            _Context = context;
        }
        public IActionResult Index()
        {
            var data = _Context.Actors.ToList();
            return View(data);
        }
        //get:Actors/create
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,profilePictrueURL,FullName,Bio")] Actor actor)
        {
            if (ModelState.IsValid)
            {
                _Context.Add(actor);
                await _Context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(actor);
        }
        // GET: Actors1/Details/1
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _Context.Actors == null)
            {
                return View("NotFound");
            }

            var actor = await _Context.Actors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (actor == null)
            {
                return View("NotFound");
            }

            return View(actor);
        }
        // GET: Actors1/Edit/1
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _Context.Actors == null)
            {
                return View("NotFound");
            }

            var actor = await _Context.Actors.FindAsync(id);
            if (actor == null)
            {
                return View("NotFound");
            }
            return View(actor);
        }

        // POST: Actors1/Edit/1
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,profilePictrueURL,FullName,Bio")] Actor actor)
        {
            if (id != actor.Id)
            {
                return View("NotFound");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _Context.Update(actor);
                    await _Context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActorExists(actor.Id))
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
            return View(actor);
        }
        // GET: Actors1/Delete/1
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _Context.Actors == null)
            {
                return View("NotFound");
            }

            var actor = await _Context.Actors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (actor == null)
            {
                return View("NotFound");
            }

            return View(actor);
        }

        // POST: Actors1/Delete/1
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_Context.Actors == null)
            {
                return Problem("Entity set 'AppDbContext.Actors'  is null.");
            }
            var actor = await _Context.Actors.FindAsync(id);
            if (actor != null)
            {
                _Context.Actors.Remove(actor);
            }

            await _Context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ActorExists(int id)
        {
            return (_Context.Actors?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
