using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetworkMovies.Models;
using System.Diagnostics;
using System.Text.Json;
using SocialNetworkMovies.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;

namespace SocialNetworkMovies.Controllers
{
    public class DiscussionController : Controller
    {
        private readonly SocialNetworkMovies.Data.SocialNetworkMoviesContext IdentityContext = new();
        private readonly UserManager<SocialNetworkMoviesUser> _userManager;

        public DiscussionController(UserManager<SocialNetworkMoviesUser> userManager)
        {
            _userManager = userManager;
        }
        private readonly SndbContext context = new();

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Details()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetDiscussionId(int Id)
        {
            string userId = _userManager.GetUserId(User);
            var user = (from u in IdentityContext.Users
                        where u.Id == userId
                        select new
                        {
                            IdUser = u.Id,
                            StrUserName = u.UserName
                        }).FirstOrDefault();
            var discussion = (from d in context.Discussions
                              select new
                              {
                                  Id = d.Id,
                                  MovieId = d.FkIdMovie,
                                  Text = d.StrText,
                                  DatePosted = d.DateCreated,
                                  IdUserCreated = d.FkIdUserCreated
                              }).Where(d => d.Id == Id).FirstOrDefault();
            if (discussion == null)
            {
                return Json("");
            }
            var objects = new { User = user, Discussion = discussion };
            return Json(objects);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                string userId = _userManager.GetUserId(User);
                Discussion discussion = new()
                {
                    FkIdMovie = int.Parse(collection["idMovie"]),
                    StrText = collection["commentText"],
                    DateCreated = DateTime.Now,
                    DateLastChanged = DateTime.Now,
                    StrState = "Ativo",
                    FkIdUserCreated = userId

                };

                // Add the new object to the Orders collection.
                context.Discussions.Add(discussion);
                context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return Redirect(nameof(Index));
        }

        public JsonResult GetLastDiscussions()
        {
            var discussions = (from d in context.Discussions
                               select new
                               {
                                   Id = d.Id,
                                   MovieId = d.FkIdMovie,
                                   Text = d.StrText,
                                   DatePosted = d.DateCreated,
                               }).OrderByDescending(d => d.Id)
            .Take(10).ToList();
            string userId = _userManager.GetUserId(User);
            var user = (from u in IdentityContext.Users
                        where u.Id == userId
                        select new
                        {
                            IdUser = u.Id,
                            StrUserName = u.UserName
                        }).FirstOrDefault();

            var objects = new { Discussions = discussions, User = user };
            return Json(objects);
        }

        public JsonResult GetLastDiscussionsNews()
        {
            string userId = _userManager.GetUserId(User);
            var discussions = (from d in context.Discussions
                               select new
                               {
                                   Id = d.Id,
                                   MovieId = d.FkIdMovie,
                                   Text = d.StrText,
                                   DatePosted = d.DateCreated,
                               }).OrderByDescending(d => d.DatePosted)
            .Take(10).ToList();
            var data = JsonSerializer.Serialize(discussions);
            return Json(data);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
