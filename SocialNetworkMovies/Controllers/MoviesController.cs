using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Text.Json;
using SocialNetworkMovies.Models;
using Microsoft.AspNetCore.Authorization;

namespace SocialNetworkMovies.Controllers
{
    [Authorize]
    public class MoviesController : Controller
    {
        readonly string key = SocialNetworkMovies.FileHandler.FileHandler.readFile();

        // GET: MoviesController
        public ActionResult Index()
        {
            return View();
        }

        // GET: MoviesController/Details/5
        public ActionResult MovieDetails(int id)
        {
            return View();
        }

        public async Task<IActionResult> GetMoviesPopular()
        {
            /* regex for emails: ((\w)|(\W))*\@@ipportalegre\.pt */
            var options = new RestClientOptions("https://api.themoviedb.org/3/movie/popular?language=en-US&page=1");
            var client = new RestClient(options);
            var request = new RestRequest("");
            request.AddHeader("accept", "application/json");
            request.AddHeader("Authorization", "Bearer " + key);
            var response = JsonSerializer.Serialize(await client.GetAsync(request));

            return Json(response);
        }

        public async Task<IActionResult> GetMovieId(int id)
        {
            /* regex for emails: ((\w)|(\W))*\@@ipportalegre\.pt */
            var options = new RestClientOptions("https://api.themoviedb.org/3/movie/" + id + "?language=en-US");
            var client = new RestClient(options);
            var request = new RestRequest("");
            request.AddHeader("accept", "application/json");
            request.AddHeader("Authorization", "Bearer " + key);
            var response = await client.GetAsync(request);

            return Json(response);
        }

        // GET: MoviesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MoviesController/Create
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

        // GET: MoviesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MoviesController/Edit/5
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

        // GET: MoviesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MoviesController/Delete/5
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
