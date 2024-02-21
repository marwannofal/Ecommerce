
using Ecommerce.Data;
using Ecommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using System.Linq;


namespace Ecommerce.Controllers
{
    public class MoviesController : Controller
    {
        private readonly AppDbContext _Context;
        public MoviesController(AppDbContext context)
        {
            _Context = context;
        }
        public async Task<IActionResult> Index()
        {
            var allMovies = await _Context.Movies.OrderBy(n => n.Name).ToListAsync();

            return View(allMovies);
        }
        //get:Actors/create
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,Name,Discription,Price,ImageURL,StartDate,EndDate,MovieCategory")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                _Context.Add(movie);
                await _Context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }
        // GET: Movies1/Details/1
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _Context.Movies == null)
            {
                return View("NotFound");
            }

            var movie = await _Context.Movies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return View("NotFound");
            }

            return View(movie);
        }
        // GET: Movies1/Edit/1
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _Context.Movies == null)
            {
                return View("NotFound");
            }

            var movie = await _Context.Movies.FindAsync(id);
            if (movie == null)
            {
                return View("NotFound");
            }
            return View(movie);
        }

        // POST: Movies1/Edit/1
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Discription,Price,ImageURL,StartDate,EndDate,MovieCategory")] Movie movie)
        {
            if (id != movie.Id)
            {
                return View("NotFound");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _Context.Update(movie);
                    await _Context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.Id))
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
            return View(movie);
        }
        private bool MovieExists(int id)
        {
            return (_Context.Actors?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Access");
        }
        [AllowAnonymous]
        public async Task<IActionResult> Filter(string searchString )
        {
            var allMovies = await _Context.Movies.ToListAsync();

            if (!string.IsNullOrEmpty(searchString))
            {
                var filteredResultNew = allMovies.Where(n => string.Equals(n.Name, searchString, StringComparison.CurrentCultureIgnoreCase) 
                || string.Equals(n.Discription, searchString, StringComparison.CurrentCultureIgnoreCase)).ToList();

                return View("Index", filteredResultNew);
            }

            return View("Index", allMovies);
        }
    }
}
