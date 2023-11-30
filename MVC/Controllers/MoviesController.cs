using Business.Models;
using Business.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MVC.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMovieService _movieService;
        private readonly IDirectorService _directorService;
        private readonly IGenraService _genrasService;

        public MoviesController(IMovieService movieService, IDirectorService directorsService, IGenraService genresService)
        {
            _movieService = movieService;
            _directorService = directorsService;
            _genrasService = genresService;
        }

        // GET: Movies/List
        public IActionResult List()
        {
            var movies = _movieService.GetList();
            return View(movies);
        }

        // GET: Movies/Create
        public IActionResult Create()
        {

            ViewBag.Directors = new SelectList(_directorService.Query()
                .Where(d => !d.IsRetired)
                .Select(d => new {
                    Id = d.Id,
                    FullName = d.Name + " " + d.Surname
                }).ToList(), "Id", "FullName");
            ViewBag.Genras = new SelectList(_genrasService.Query(), "Id", "Name");
            return View();
        }

        // POST: Movies/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(MovieModel movie)
        {
            if (ModelState.IsValid)
            {
                var result = _movieService.Add(movie);
                if (result)
                {
                    return RedirectToAction(nameof(List));
                }
                ModelState.AddModelError("", "Movie has been added.");
            }

            // Preserve the directors and genres selections in case of a validation error
            ViewBag.Directors = new SelectList(_directorService.Query()
                .Where(d => !d.IsRetired)
                .Select(d => new {
                    Id = d.Id,
                    FullName = d.Name + " " + d.Surname
                }).ToList(), "Id", "FullName");
            ViewBag.Genras = new SelectList(_genrasService.Query(), "Id", "Name");
            return View(movie);
        }

        // GET: Movies/Edit/5
        public IActionResult Edit(int id)
        {
            var movie = _movieService.GetItem(id);
            if (movie == null)
            {
                return NotFound();
            }

            ViewBag.Directors = new SelectList(_directorService.Query()
                .Where(d => !d.IsRetired)
                .Select(d => new {
                    Id = d.Id,
                    FullName = d.Name + " " + d.Surname
                }).ToList(), "Id", "FullName");
            ViewBag.Genras = new SelectList(_genrasService.Query(), "Id", "Name");
            return View(movie);
        }

        // POST: Movies/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, MovieModel movie)
        {
            if (id != movie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var result = _movieService.Update(movie);
                if (result)
                {
                    return RedirectToAction(nameof(List));
                }
                ModelState.AddModelError("", "Movie has been updated.");
            }

            ViewBag.Directors = new SelectList(_directorService.Query()
                .Where(d => !d.IsRetired)
                .Select(d => new {
                    Id = d.Id,
                    FullName = d.Name + " " + d.Surname
                }).ToList(), "Id", "FullName");
            ViewBag.Genras = new SelectList(_genrasService.Query(), "Id", "Name");
            return View(movie);
        }

        // GET: Movies/Details/5
        public IActionResult Details(int id)
        {
            var movie = _movieService.GetItem(id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // GET: Movies/Delete/5
        public IActionResult Delete(int id)
        {
            var movie = _movieService.GetItem(id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var result = _movieService.Delete(id);
            if (result)
            {
                return RedirectToAction(nameof(List));
            }

            ModelState.AddModelError("", "Movie could not be deleted.");
            return View(_movieService.GetItem(id));
        }
    }
}
