using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialNetworkMovies.Models;
using System.Text.Json;

namespace SocialNetworkMovies.Controllers
{
    public class CommentController : Controller
    {
        private readonly SndbContext context = new();
        // GET: CommentController
        public ActionResult Index()
        {
            return View();
        }

        // GET: CommentController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        public JsonResult LoadRepliesCommentParentId(int Id, int Pagination)
        {
            var replies = (from r in context.Comments
                           join d in context.Discussions
                           on r.FkIdDiscussion equals d.Id
                           select new
                           {
                               IdComment = r.Id,
                               TextComment = r.TextComment,
                               DatePosted = r.DateCreated,
                               FkIdDiscussion = r.FkIdDiscussion,
                               IdCommentParent = r.FkIdComment
                           }).Where(c => c.FkIdDiscussion == Id)
            .OrderByDescending(c => c.IdComment)
            .Skip(Pagination * 10).Take(10).ToList();

            var data = JsonSerializer.Serialize(replies);
            return Json(data);
        }

        [HttpGet]
        public JsonResult LoadCommentsDiscussion(int Id, int Pagination)
        {
            var comments = (from c in context.Comments
                            join d in context.Discussions
                            on c.FkIdDiscussion equals d.Id
                            select new
                            {
                                IdComment = c.Id,
                                TextComment = c.TextComment,
                                DatePosted = c.DateCreated,
                                FkIdDiscussion = c.FkIdDiscussion,
                                IdCommentParent = c.FkIdComment,
                            }).Where(c => c.FkIdDiscussion == Id && c.IdCommentParent == null)
            .OrderByDescending(c => c.IdComment)
            .Skip(Pagination * 10).Take(10).ToList();

            var replies = (from r in context.Comments
                           join d in context.Discussions
                           on r.FkIdDiscussion equals d.Id
                           select new
                           {
                               IdComment = r.Id,
                               TextComment = r.TextComment,
                               DatePosted = r.DateCreated,
                               FkIdDiscussion = r.FkIdDiscussion,
                               IdCommentParent = r.FkIdComment,
                           }).Where(r => r.FkIdDiscussion == Id && r.IdCommentParent != null)
                            .OrderByDescending(r => r.IdComment)
                            .Skip(Pagination * 10).Take(10).ToList();

            var objects = new { Comments = comments, Replies = replies };
            string data = JsonSerializer.Serialize(objects);
            return Json(data);
        }

        public JsonResult GetReplies(int IdDiscussion, int idReply, int Pagination)
        {
            var replies = (from r in context.Comments
                           join d in context.Discussions
                           on r.FkIdDiscussion equals d.Id
                           select new
                           {
                               IdComment = r.Id,
                               TextComment = r.TextComment,
                               DatePosted = r.DateCreated,
                               FkIdDiscussion = r.FkIdDiscussion,
                               IdCommentParent = r.FkIdComment,
                           }).Where(r => r.FkIdDiscussion == IdDiscussion && r.IdCommentParent == idReply)
                            .OrderByDescending(r => r.IdComment)
                            .Skip(Pagination * 10).Take(10).ToList();

            string data = JsonSerializer.Serialize(replies);
            return Json(data);
        }
        [HttpPost]
        public JsonResult PostReply(IFormCollection collection)
        {
            try
            {
                if (int.Parse(collection["FkIdCommentParent"]) == null)
                {
                    return Json("{\"success\": \"false\"}");
                }

                Comment comment = new()
                {
                    StrName = "Remove this later",
                    FkIdDiscussion = int.Parse(collection["IdDiscussion"]),
                    TextComment = collection["Text"],
                    FkIdComment = int.Parse(collection["FkIdCommentParent"]),
                    DateCreated = DateTime.Now,
                    DateLastChanged = DateTime.Now,
                    StrState = "Activo"
                };

                // Add the new object to the Orders collection.
                context.Comments.Add(comment);
                context.SaveChanges();
                return Json("{\"success\": true}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Json("{\"success\": false}");
            }
        }

        [HttpPost]
        public JsonResult PostComment(IFormCollection collection)
        {
            try
            {
                Comment comment = new()
                {
                    StrName = "Remove this later",
                    FkIdDiscussion = !string.IsNullOrEmpty(collection["IdDiscussion"]) ? int.Parse(collection["IdDiscussion"]) : null,
                    TextComment = collection["TextComment"],
                    DateCreated = DateTime.Now,
                    DateLastChanged = DateTime.Now,
                    StrState = "Activo"
                };

                // Add the new object to the Orders collection.
                context.Comments.Add(comment);
                context.SaveChanges();
                return Json("{\"success\": true, \"commentId\":" + comment.Id + "}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Json("{\"success\": false}");
            }
        }

        // GET: CommentController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CommentController/Create
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

        // GET: CommentController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CommentController/Edit/5
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

        // GET: CommentController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CommentController/Delete/5
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
