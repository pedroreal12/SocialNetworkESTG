using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetworkMovies.Models;
using System.Diagnostics;
using System.Text.Json;

namespace SocialNetworkMovies.Controllers
{
    public class ListController : Controller
    {
        private readonly ILogger<ListController> _logger;
        private readonly SndbContext context = new();
        private readonly SocialNetworkMovies.Data.SocialNetworkMoviesContext IdentityContext = new();

        public ListController(ILogger<ListController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Details()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Edit()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Delete()
        {
            return View();
        }

        public JsonResult GetLists()
        {
            var lists = (from l in context.UserLists
                         join u in IdentityContext.Users
                         on l.FkIdUserCreated equals u.Id
                         select new
                         {
                             Id = l.Id,
                             StrNameList = l.StrName,
                             UserStrName = u.UserName
                         }).OrderByDescending(l => l.Id).Take(10).ToList();
            var data = JsonSerializer.Serialize(lists);
            return Json(data);
        }

        public JsonResult GetListId(int Id)
        {
            var list = (from l in context.UserLists
                        join u in IdentityContext.Users
                        on l.FkIdUserCreated equals u.Id
                        join ml in context.MovieLists
                        on l.FkIdMovieList equals ml.Id
                        select new
                        {
                            Id = l.Id,
                            StrNameList = l.StrName,
                            UserStrName = u.UserName
                        }).Where(l => l.Id == Id).ToList();
            var data = JsonSerializer.Serialize(list);
            return Json(data);
        }
    }
}
