using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetworkMovies.Models;
using System.Diagnostics;
using System.Text.Json;

namespace SocialNetworkMovies.Controllers
{
    public class DiscussionController : Controller
    {
        private readonly SndbContext context = new();
        private readonly ILogger<DiscussionController> _logger;

        public DiscussionController(ILogger<DiscussionController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                Discussion discussion = new()
                {
                    FkIdMovie = int.Parse(collection["idMovie"]),
                    StrText = collection["commentText"],
                    DateCreated = DateTime.Now,
                    DateLastChanged = DateTime.Now,
                    StrState = "Activo"
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
                                   DatePosted = d.DateCreated
                               }).OrderByDescending(d => d.Id)
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
