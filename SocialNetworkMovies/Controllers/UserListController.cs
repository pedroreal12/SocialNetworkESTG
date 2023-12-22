using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SocialNetworkMovies.Controllers
{
    public class UserListController : Controller
    {
        // GET: UserListController
        public ActionResult Index()
        {
            return View();
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

        // POST: UserListController/Create
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
