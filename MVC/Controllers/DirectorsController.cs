using Business.Models;
using Business.Services;
using DataAccess.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
    public class DirectorsController : Controller
    {
        private readonly IDirectorService _directorService;

        public DirectorsController(IDirectorService directorService)
        {
            _directorService = directorService;
        }

        // GET: Directors/List
        public IActionResult List()
        {
            List<DirectorModel> directorList = _directorService.Query().ToList();
            return View("List", directorList);
        }

        // GET: Directors/Details/5
        public IActionResult Details(int id)
        {
            DirectorModel director = _directorService.Query().SingleOrDefault(d => d.Id == id);
            if (director == null)
            {
                return NotFound();
            }

            return View(director);
        }

        // GET: Directors/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Directors/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(DirectorModel director)
        {
            if (ModelState.IsValid)
            {
                var result = _directorService.Add(director);
                if (result)
                {
                    TempData["Message"] = "Director has been added.";
                    return RedirectToAction(nameof(List));
                }

                ModelState.AddModelError("", "Director could not be added.");
            }

            return View(director);
        }

        // GET: Directors/Edit/5
        public IActionResult Edit(int id)
        {
            DirectorModel director = _directorService.Query().SingleOrDefault(d => d.Id == id);
            if (director == null)
            {
                return NotFound();
            }

            return View(director);
        }

        // POST: Directors/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(DirectorModel director)
        {
            if (ModelState.IsValid)
            {
                var result = _directorService.Update(director);
                if (result)
                {
                    TempData["Message"] = "Director has been updated.";
                    return RedirectToAction(nameof(List));
                }

                ModelState.AddModelError("", "Director could not be updated.");
            }

            return View(director);
        }

        // GET: Directors/Delete/5
        public IActionResult Delete(int id)
        {
            DirectorModel director = _directorService.Query().SingleOrDefault(d => d.Id == id);
            if (director == null)
            {
                return NotFound();
            }

            return View(director);
        }

        // POST: Directors/Delete
        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var result = _directorService.Delete(id);
            if (result)
            {
                TempData["Message"] = "Director has been deleted.";
                return RedirectToAction(nameof(List));
            }

            ModelState.AddModelError("", "Director could not be deleted.");
            return RedirectToAction(nameof(List));
        }
    }
}
