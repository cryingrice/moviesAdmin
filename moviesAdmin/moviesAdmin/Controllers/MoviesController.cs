using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using moviesAdmin.Models;
using MoviesAdmin;
using MoviesAdmin.Services;

namespace moviesAdmin.Controllers
{
    public class MoviesController(MoviesAdminContext context, AzureBlobService azureBlobService, ILogger<MoviesController> logger) : Controller
    {
        private readonly MoviesAdminContext _context = context;
        private readonly AzureBlobService _blobService = azureBlobService;
        private readonly ILogger<MoviesController> _logger = logger;

        // GET: Movies
        public async Task<IActionResult> Index()
        {
            var movies = await _context.Movies
            .Include(m => m.Genre)
            .ToListAsync();
            return View(movies);
        }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
                .Include(m => m.Genre)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // GET: Movies/Create
        public IActionResult Create()
        {
            ViewBag.Genres = GetGenreOptions();
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Id,Title,GenreId,AudienceRatingId,Synopsis,Runtime,ReleaseDate,PosterUrl")] Movie movie,
            IFormFile? posterFile)
        {

            if (posterFile == null || posterFile.Length == 0)
            { 
                ModelState.AddModelError("PosterFile", "Please select an image file to upload.");
                return View(movie);
            }
            if (ModelState.IsValid) // validate input
            {

                if (posterFile != null && posterFile.Length > 0)
                {

                    
                    var filename = System.IO.Path.GetFileName(posterFile.FileName);
                    
                    string extension = Path.GetExtension(filename);
                    string newFileName = $"{Guid.NewGuid()}{extension}";

                    
                    string url = await _blobService.UploadFileAsync(posterFile, newFileName);
                    


                    
                    movie.PosterUrl = url;
                }

                _context.Add(movie); // add new movie to context (db)
                await _context.SaveChangesAsync(); // save context changes in db

                return RedirectToAction(nameof(Index)); // re-direct to Movies/Index page
            }

            return View(movie);
        }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            ViewBag.Genres = GetGenreOptions();
            return View(movie);
        }

        // POST: Movies/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,GenreId,AudienceRatingId,Synopsis,Runtime,ReleaseDate,PosterUrl")] Movie movie)
        {
            if (id != movie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.Id))
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
            return View(movie);
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie != null)
            {
                _context.Movies.Remove(movie);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.Id == id);
        }

        private List<SelectListItem> GetGenreOptions()
        {
            var genres = _context.Genres
                .Select(g => new SelectListItem
                {
                    Text = g.Title,
                    Value = g.Id.ToString()
                })
                .ToList();

            return genres;
        }
    }
}
