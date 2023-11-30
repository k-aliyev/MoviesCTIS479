using Business.Models;
using Business.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
    public class GanresController : Controller
    {
        private readonly IGanreService _ganreService;

        public GanresController(IGanreService ganreService)
        {
            _ganreService = ganreService;
        }

        // GET: Ganres/List
        public IActionResult List()
        {
            List<GanreModel> ganreList = _ganreService.Query().ToList();
            return View("List", ganreList);
        }

        // GET: Ganres/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Ganres/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(GanreModel ganre)
        {
            if (ModelState.IsValid)
            {
                bool result = _ganreService.Add(ganre);
                if (result)
                {
                    TempData["Message"] = "Genre has been added.";
                    return RedirectToAction(nameof(List));
                }

                ModelState.AddModelError("", "Genre could not be added.");
            }

            return View(ganre);
        }

        // GET: Ganres/Delete/5
        public IActionResult Delete(int id)
        {
            GanreModel ganre = _ganreService.Query().SingleOrDefault(g => g.Id == id);
            if (ganre == null)
            {
                return NotFound();
            }

            return View(ganre);
        }

        // POST: Ganres/Delete
        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            bool result = _ganreService.Delete(id);
            if (result)
            {
                TempData["Message"] = "Genre has been deleted.";
                return RedirectToAction(nameof(List));
            }

            ModelState.AddModelError("", "Genre could not be deleted.");
            return RedirectToAction(nameof(List));
        }
    }
}
