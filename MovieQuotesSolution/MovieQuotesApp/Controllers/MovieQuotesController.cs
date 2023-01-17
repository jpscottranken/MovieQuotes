using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MovieQuotesApp.Data;
using MovieQuotesApp.Models;

namespace MovieQuotesApp.Controllers
{
    public class MovieQuotesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MovieQuotesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MovieQuotes
        [Authorize]
        public async Task<IActionResult> Index()
        {
              return View(await _context.MovieQuotes.ToListAsync());
        }

        // GET: MovieQuotes/AuthorSearch
        [Authorize]
        public async Task<IActionResult> AuthorSearch()
        {
            return View();
        }

        // POST: MovieQuotes/AuthorSearchResults
        [Authorize]
        public async Task<IActionResult> AuthorSearchResults(string searchTerm)
        {
            return View("Index", 
            await _context.MovieQuotes.Where(q => q.QuoteAuthor.Contains(searchTerm)).ToListAsync());
        }

        // GET: MovieQuotes/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MovieQuotes == null)
            {
                return NotFound();
            }

            var movieQuotes = await _context.MovieQuotes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movieQuotes == null)
            {
                return NotFound();
            }

            return View(movieQuotes);
        }

        // GET: MovieQuotes/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: MovieQuotes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,QuoteMovie,QuoteText,QuoteAuthor")] MovieQuotes movieQuotes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movieQuotes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(movieQuotes);
        }

        // GET: MovieQuotes/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MovieQuotes == null)
            {
                return NotFound();
            }

            var movieQuotes = await _context.MovieQuotes.FindAsync(id);
            if (movieQuotes == null)
            {
                return NotFound();
            }
            return View(movieQuotes);
        }

        // POST: MovieQuotes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,QuoteMovie,QuoteText,QuoteAuthor")] MovieQuotes movieQuotes)
        {
            if (id != movieQuotes.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movieQuotes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieQuotesExists(movieQuotes.Id))
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
            return View(movieQuotes);
        }

        // GET: MovieQuotes/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MovieQuotes == null)
            {
                return NotFound();
            }

            var movieQuotes = await _context.MovieQuotes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movieQuotes == null)
            {
                return NotFound();
            }

            return View(movieQuotes);
        }

        // POST: MovieQuotes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MovieQuotes == null)
            {
                return Problem("Entity set 'ApplicationDbContext.MovieQuotes'  is null.");
            }
            var movieQuotes = await _context.MovieQuotes.FindAsync(id);
            if (movieQuotes != null)
            {
                _context.MovieQuotes.Remove(movieQuotes);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieQuotesExists(int id)
        {
          return _context.MovieQuotes.Any(e => e.Id == id);
        }
    }
}
