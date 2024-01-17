using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialNetworkMovies.Models;
using SocialNetworkMovies.Data;
using System.Text.Json;
using SocialNetworkMovies.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;

namespace SocialNetworkMovies.Controllers
{
    public class MovieListController : Controller
    {
        private readonly SndbContext context = new();
        private readonly UserManager<SocialNetworkMoviesUser> _userManager;

        public MovieListController(UserManager<SocialNetworkMoviesUser> userManager)
        {
            _userManager = userManager;
        }
        // GET: MovieListController
        public ActionResult Index()
        {
            return View();
        }

        // GET: MovieListController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MovieListController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MovieListController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MovieListController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MovieListController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MovieListController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MovieListController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
