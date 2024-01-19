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

        [HttpGet]
        public JsonResult RemoveMovieFromList(int IdMovieList, int IdMovie)
        {
            if (IdMovieList <= 0)
            {
                return Json("{\"success\": true, \"message\": \"Movie List not valid\"}");
            }

            try
            {
                Console.WriteLine($"IdMovieList: {IdMovieList}\nIdMovie: {IdMovie}");
                var movieListToRemove = context.MovieLists.Find(IdMovieList);
                if (movieListToRemove == null)
                {
                    return Json("{\"success\": false, \"message\": \"Movie not Found\"}");
                }
                Console.WriteLine($"MovieList: {movieListToRemove}");
                movieListToRemove.StrState = "Apagado";
                context.Update(movieListToRemove);
                context.SaveChanges();
                return Json("{\"success\": true, \"message\": \"Movie Removed successfully from the list\"}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Json("{\"success\": false, \"message\": \"Error on removing movie from the list\"}");
            }
        }

        public async Task<JsonResult> GetMoviesByListId(int IdUserList)
        {
            if (IdUserList <= 0)
            {
                return Json("{\"success\": false, \"message\": \"List not found\"}");
            }
            Console.WriteLine($"IdUserList: {IdUserList}");
            var movieList = (from ml in context.MovieLists
                             join ul in context.UserLists
                             on ml.FkIdList equals ul.Id
                             select new
                             {
                                 IdMovieList = ml.Id,
                                 IdMovie = ml.FkIdMovie,
                                 IdUserList = ml.FkIdList,
                                 StrListName = ul.StrName,
                                 DateCreated = ul.DateCreated,
                                 StrStatus = ml.StrState
                             }).Where(ml => ml.StrStatus == "Ativo" && ml.IdUserList == IdUserList).ToList();
            if (!movieList.Any())
            {
                return Json("{\"success\": false, \"message\": \"List not found\"}");
            }
            List<string> data = new List<string>();
            foreach (var ml in movieList)
            {
                var options = new RestClientOptions("https://api.themoviedb.org/3/movie/" + ml.IdMovie);
                var client = new RestClient(options);
                var request = new RestRequest("");
                request.AddHeader("accept", "application/json");
                request.AddHeader("Authorization", "Bearer " + key);
                var response = await client.GetAsync(request);

                data.Add(response.Content);
            }
            var objects = new { Data = data, MovieList = movieList };
            return Json(objects);
        }

        public JsonResult AddMovieToUserList(int IdUserList, int IdMovie)
        {
            try
            {
                var movieListCheck = (from ml in context.MovieLists
                                      join ul in context.UserLists
                                      on ml.FkIdList equals ul.Id
                                      select new
                                      {
                                          IdUserList = ml.FkIdList,
                                          IdMovie = ml.FkIdMovie,
                                          StrStatus = ml.StrState
                                      }).Where(ml => ml.IdUserList == IdUserList && ml.IdMovie == IdMovie && ml.StrStatus == "Ativo").FirstOrDefault();
                if (movieListCheck != null)
                {
                    return Json("{\"success\": false, \"message\": \"Movie is already in this list\"}");
                }
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
                return Json("{\"success\": true, \"message\": \"Movie successfully added to the list!\"}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Json("{\"success\": false, \"message\": \"Error on adding movie to the list!\"}");
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
