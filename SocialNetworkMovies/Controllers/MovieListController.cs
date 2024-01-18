using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialNetworkMovies.Models;
using SocialNetworkMovies.Data;
using System.Text.Json;
using SocialNetworkMovies.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using RestSharp;

namespace SocialNetworkMovies.Controllers
{
    public class MovieListController : Controller
    {
        readonly static string key = SocialNetworkMovies.FileHandler.FileHandler.readFile();
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
        public ActionResult Details(int Id)
        {
            return View();
        }

        public async Task<JsonResult> GetMoviesByListId(int IdMovieList)
        {
            var movieList = (from ml in context.MovieLists
                             join ul in context.UserLists
                             on ml.FkIdList equals ul.Id
                             select new
                             {
                                 IdMovie = ml.FkIdMovie,
                                 IdUserList = ul.Id,
                             }).Take(10).ToList();
            string data = "";
            foreach (var ml in movieList)
            {
                var options = new RestClientOptions("https://api.themoviedb.org/3/movie/" + ml.IdMovie);
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
                data += JsonSerializer.Serialize(response);
            }
            return Json(data);
        }

        // GET: MovieListController/Create
        public ActionResult Create()
        {
            return View();
        }

        public JsonResult AddMovieToUserList(int IdUserList, int IdMovie)
        {
            try
            {
                string userId = _userManager.GetUserId(User);
                MovieList movieList = new()
                {
                    FkIdList = IdUserList,
                    FkIdMovie = IdMovie,
                    DateCreated = DateTime.Now,
                    DateLastChanged = DateTime.Now,
                    StrState = "Ativo",
                    FkIdUserCreated = userId,
                };

                context.MovieLists.Add(movieList);
                context.SaveChanges();
                return Json("{\"success\": true}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Json("{\"success\": false}");
            }
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
