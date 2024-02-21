using Ecommerce.Data;
using Ecommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Controllers
{
    public class ProducersController : Controller
    {
        private readonly AppDbContext _Context;

        public ProducersController(AppDbContext context)
        {
            _Context = context;
        }
        public async Task<IActionResult> Index()
        {
            var allProducers =await _Context.Producers.ToListAsync();

            return View(allProducers);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,ProfilePictureURL,FullName,Bio")] Producer producer)
        {
            if (ModelState.IsValid)
            {
                _Context.Add(producer);
                await _Context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(producer);
        }
        // GET: Producers1/Details/1
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _Context.Producers == null)
            {
                return View("NotFound");
            }

            var producer = await _Context.Producers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (producer == null)
            {
                return View("NotFound");
            }

            return View(producer);
        }
        // GET: Producers1/Edit/1
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _Context.Actors == null)
            {
                return View("NotFound");
            }

            var producer = await _Context.Producers.FindAsync(id);
            if (producer == null)
            {
                return View("NotFound");
            }
            return View(producer);
        }

        // POST: Producers1/Edit/1
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProfilePictureURL,FullName,Bio")] Producer producer)
        {
            if (id != producer.Id)
            {
                return View("NotFound");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _Context.Update(producer);
                    await _Context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProduceExists(producer.Id))
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
            return View(producer);
        }
        // GET: Producers1/Delete/1
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _Context.Producers == null)
            {
                return View("NotFound");
            }

            var producer = await _Context.Producers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (producer == null)
            {
                return View("NotFound");
            }

            return View(producer);
        }

        // POST: Producers1/Delete/1
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_Context.Producers == null)
            {
                return Problem("Entity set 'AppDbContext.Producers'  is null.");
            }
            var producer = await _Context.Producers.FindAsync(id);
            if (producer != null)
            {
                _Context.Producers.Remove(producer);
            }

            await _Context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool ProduceExists(int id)
        {
            throw new NotImplementedException();
        }
    }
}
