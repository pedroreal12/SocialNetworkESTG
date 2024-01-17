using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialNetworkMovies.Models;
using SocialNetworkMovies.Data;
using System.Text.Json;
using SocialNetworkMovies.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;


namespace SocialNetworkMovies.Controllers
{
    public class UserListController : Controller
    {
        private readonly SndbContext context = new();
        private readonly UserManager<SocialNetworkMoviesUser> _userManager;
        // GET: UserListController
        //
        public UserListController(UserManager<SocialNetworkMoviesUser> userManager)
        {
            _userManager = userManager;
        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetUserLists()
        {
            string userId = _userManager.GetUserId(User);
            var lists = (from l in context.UserLists
                         where l.FkIdUserCreated == userId
                         select new
                         {
                             IdList = l.Id,
                             StrNameList = l.StrName
                         }).OrderByDescending(l => l.IdList).Take(10).ToList();

            string data = JsonSerializer.Serialize(lists);
            return Json(data);
        }

        public JsonResult GetListById(int Id)
        {
            string userId = _userManager.GetUserId(User);
            var list = (from l in context.UserLists
                        join ml in context.MovieLists
                        on l.FkIdMovieList equals ml.Id
                        where l.Id == Id && l.FkIdUserCreated == userId
                        select new
                        {
                            IdList = l.Id,
                            StrNameList = l.StrName,
                            IdMovie = ml.FkIdMovie
                        }).GroupBy(l => l.IdList).FirstOrDefault();

            return Json(JsonSerializer.Serialize(list));
        }

        // GET: UserListController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserListController/Create
        public ActionResult Create()
        {
            return View();
        }

        public JsonResult PostReview(int Value, int IdMovie, int FkIdComment)
        {
            try
            {
                Review review = new()
                {
                    FkIdMovie = IdMovie,
                    IntValue = Value,
                    FkIdComment = FkIdComment,
                    DateCreated = DateTime.Now,
                    DateLastChanged = DateTime.Now,
                    StrState = "Ativo"
                };

                // Add the new object to the Orders collection.
                context.Reviews.Add(review);
                context.SaveChanges();
                return Json("{\"success\": true}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Json("{\"success\": false}");
            }
        }

        // POST: UserListController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                UserList userList = new UserList()
                {
                };
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserListController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserListController/Edit/5
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

        // GET: UserListController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserListController/Delete/5
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
