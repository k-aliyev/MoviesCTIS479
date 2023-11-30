using Business.Models;
using Business.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
    public class GenrasController : Controller
    {
        private readonly IGenraService _genraService;

        public GenrasController(IGenraService genraService)
        {
            _genraService = genraService;
        }

        // GET: Genras/List
        public IActionResult List()
        {
            List<GenraModel> genraList = _genraService.Query().ToList();
            return View("List", genraList);
        }

        // GET: Genras/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Genras/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(GenraModel genra)
        {
            if (ModelState.IsValid)
            {
                bool result = _genraService.Add(genra);
                if (result)
                {
                    TempData["Message"] = "Genre has been added.";
                    return RedirectToAction(nameof(List));
                }

                ModelState.AddModelError("", "Genre could not be added.");
            }

            return View(genra);
        }

        // GET: Genras/Delete/5
        public IActionResult Delete(int id)
        {
            GenraModel genra = _genraService.Query().SingleOrDefault(g => g.Id == id);
            if (genra == null)
            {
                return NotFound();
            }

            return View(genra);
        }

        // POST: Genras/Delete
        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            bool result = _genraService.Delete(id);
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
