﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using SocialNetworkMovies.Models;
using SocialNetworkMovies.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace SocialNetworkMovies.Controllers
{
    [Authorize]
    public class ReviewController : Controller
    {
        private readonly SocialNetworkMovies.Data.SocialNetworkMoviesContext IdentityContext = new();
        private readonly SndbContext context = new();
        private readonly UserManager<SocialNetworkMoviesUser> _userManager;
        // GET: CommentController
        public ReviewController(UserManager<SocialNetworkMoviesUser> userManager)
        {
            _userManager = userManager;
        }
        // GET: ReviewController
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetReviews(int Pagination)
        {
            var reviews = (from r in context.Reviews
                           join c in context.Comments
                           on r.FkIdComment equals c.Id
                           select new
                           {
                               Id = r.Id,
                               IdMovie = r.FkIdMovie,
                               Text = c.TextComment,
                               Value = r.IntValue,
                               DatePosted = c.DateCreated,
                               StrState = r.StrState,
                               UserName = r.UserName,
                               IdUser = r.FkIdUserCreated
                           }).OrderByDescending(r => r.Id).Where(r => r.StrState == "Ativo").Skip(Pagination * 10).Take(10).ToList();
            var objects = new { Reviews = reviews };
            return Json(objects);
        }

        public JsonResult GetDetails(int Id)
        {
            var review = (from r in context.Reviews
                          join c in context.Comments
                          on r.FkIdComment equals c.Id
                          select new
                          {
                              Id = r.Id,
                              IdMovie = r.FkIdMovie,
                              Text = c.TextComment,
                              Value = r.IntValue,
                              DatePosted = c.DateCreated,
                              StrState = r.StrState,
                              UserName = r.UserName,
                          }).OrderByDescending(r => r.Id).Where(r => r.StrState == "Ativo" && r.Id == Id).ToList();
            var objects = new { Review = review };
            return Json(objects);
        }

        public JsonResult GetLastReviewsNews()
        {
            var reviews = (from r in context.Reviews
                           join c in context.Comments
                           on r.FkIdComment equals c.Id
                           select new
                           {
                               Id = r.Id,
                               Text = c.TextComment,
                               DatePosted = r.DateCreated,
                               StrStatus = r.StrState,
                               UserName = r.UserName,
                           }).Where(r => r.StrStatus == "Ativo").OrderByDescending(r => r.DatePosted)
            .Take(10).ToList();
            var data = JsonSerializer.Serialize(reviews);
            return Json(data);
        }

        public JsonResult PostReview(int Value, int IdMovie, int FkIdComment)
        {
            try
            {
                string userId = _userManager.GetUserId(User);
                string UserName = _userManager.GetUserName(User);
                Review review = new()
                {
                    FkIdMovie = IdMovie,
                    IntValue = Value,
                    FkIdComment = FkIdComment,
                    DateCreated = DateTime.Now,
                    FkIdUserCreated = userId,
                    UserName = UserName,
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
