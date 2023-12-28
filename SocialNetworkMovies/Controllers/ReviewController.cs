using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using SocialNetworkMovies.Models;

namespace SocialNetworkMovies.Controllers
{
    public class ReviewController : Controller
    {
        private readonly SndbContext context = new();
        // GET: ReviewController
        public ActionResult Index()
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

        // GET: ReviewController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ReviewController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ReviewController/Create
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

        // GET: ReviewController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ReviewController/Edit/5
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

        // GET: ReviewController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ReviewController/Delete/5
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
