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

        // GET: MoviesController/Details/5
        public ActionResult MovieDetails()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetMovieByTitle(string title)
        {
            var options = new RestClientOptions("https://api.themoviedb.org/3/search/movie?query=" + title);
            var client = new RestClient(options);
            var request = new RestRequest("");
            request.AddHeader("accept", "application/json");
            request.AddHeader("Authorization", "Bearer " + key);
            var response = await client.GetAsync(request);
            response.Request = null;
            response.ContentType = null;
            response.ContentHeaders = null;
            response.ContentEncoding = null;
            response.Headers = null;

            return Json(response);
        }

        public async Task<IActionResult> GetMoviesPopular()
        {
            /* regex for emails: ((\w)|(\W))*\@@ipportalegre\.pt */
            var options = new RestClientOptions("https://api.themoviedb.org/3/movie/popular?language=en-US&page=1");
            var client = new RestClient(options);
            var request = new RestRequest("");
            request.AddHeader("accept", "application/json");
            request.AddHeader("Authorization", "Bearer " + key);
            var response = await client.GetAsync(request);
            response.Request = null;
            response.ContentType = null;
            response.ContentHeaders = null;
            response.ContentEncoding = null;
            response.Headers = null;

            return Json(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetMovieId(int Id)
        {
            /* regex for emails: ((\w)|(\W))*\@@ipportalegre\.pt */
            var options = new RestClientOptions("https://api.themoviedb.org/3/movie/" + Id);
            var client = new RestClient(options);
            var request = new RestRequest("");
            request.AddHeader("accept", "application/json");
            request.AddHeader("Authorization", "Bearer " + key);
            var response = await client.GetAsync(request);
            response.Request = null;
            response.ContentType = null;
            response.ContentHeaders = null;
            response.ContentEncoding = null;
            response.Headers = null;

            return Json(response);
        }

        // GET: MoviesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

    }
}
