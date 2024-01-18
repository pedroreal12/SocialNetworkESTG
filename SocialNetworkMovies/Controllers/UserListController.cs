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
                             StrNameList = l.StrName,
                             DateCreated = l.DateCreated
                         }).OrderByDescending(l => l.IdList).Take(10).ToList();

            string data = JsonSerializer.Serialize(lists);
            return Json(data);
        }

        public JsonResult GetListsByUser()
        {
            string userId = _userManager.GetUserId(User);
            var UserLists = (from ul in context.UserLists
                             select new
                             {
                                 IdUserList = ul.Id,
                                 StrListName = ul.StrName,
                                 IdUserCreated = ul.FkIdUserCreated
                             }).Where(ul => ul.IdUserCreated == userId).ToList();
            return Json(UserLists);
        }

        [HttpGet]
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
            UserList UserList = context.UserLists.Find(id);
            return View(UserList);
        }

        // GET: UserListController/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserListController/Create
        [HttpPost]
        public JsonResult Create(IFormCollection collection)
        {
            try
            {
                string userId = _userManager.GetUserId(User);
                string StrListName = collection?["StrListName"] ?? "";

                if (StrListName == "")
                {
                    return Json("{\"success\": false}");
                }

                UserList userList = new()
                {
                    StrName = StrListName,
                    DateCreated = DateTime.Now,
                    DateLastChanged = DateTime.Now,
                    StrState = "Ativo",
                    FkIdUserCreated = userId,
                };

                context.UserLists.Add(userList);
                context.SaveChanges();
                return Json("{\"success\": true}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Json("{\"success\": false}");
            }
        }

        // GET: UserListController/Edit/5
        public ActionResult Edit(int id)
        {
            UserList UserList = context.UserLists.Find(id);
            return View(UserList);
        }

        // POST: UserListController/Edit/5
        [HttpPost]
        public JsonResult Edit(int Id, IFormCollection collection)
        {
            try
            {
                string StrListName = collection?["StrListName"] ?? "";

                if (StrListName == "")
                {
                    return Json("{\"success\": false}");
                }
                UserList UserList = context.UserLists.Find(Id);
                if (UserList == null)
                {
                    return Json("{\"success\": false}");
                }
                UserList.StrName = StrListName;
                context.Update(UserList);
                context.SaveChanges();
                return Json("{\"success\": true}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Json("{\"success\": false}");
            }
        }

        // GET: UserListController/Delete/5
        public JsonResult Delete(int id)
        {
            UserList UserList = context.UserLists.Find(id);
            if (UserList == null)
            {
                return Json("{\"success\": false}");
            }
            UserList.StrState = "Apagado";
            return Json("{\"success\": true}");
        }
    }
}
