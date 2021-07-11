using BusinessLayer.Concrete;
using EntityLayer.Concrete;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BASARSOFTHABER.Controllers
{
    public class AdminController : Controller
    {
        NewsManager nm = new NewsManager();
        AuthorManager autman = new AuthorManager();
        // GET: Admin
        public ActionResult AdminNewsList(int? page)
        {
            var anewslist = nm.GetAll().ToPagedList(page ?? 1, 10);
            return View(anewslist);
        }
        [HttpGet]
        [Authorize(Roles ="Admin")]
        public ActionResult AddNewNews()
        {
            NewsDataBaseEntities c = new NewsDataBaseEntities();
            List<SelectListItem> values = (from x in c.Categories.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.CategoryName,
                                               Value = x.id.ToString()
                                           }).ToList();

            List<SelectListItem> values2 = (from x in c.Authors.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.User.Name +" "+ x.User.Surname,
                                               Value = x.id.ToString()
                                           }).ToList();
            ViewBag.values = values;
            ViewBag.values2 = values2;
            return View();
        }
        [HttpPost]
        public ActionResult AddNewNews(News p)
        {
            if (Request.Files != null && Request.Files.Count > 0)
            {
                var file = Request.Files[0];
                if (file.ContentLength > 0)
                {
                    var folder = Server.MapPath("~/images/newsuploads/");
                    var filename = Guid.NewGuid() + ".jpg";
                    file.SaveAs(Path.Combine(folder, filename));

                    var filePath = "~/images/newsuploads/" + filename;
                    p.Image = filePath;
                }

            }
            p.NewsRating = 0;
            p.IsDeleted = false;
            nm.NewsAddBl(p);
            return RedirectToAction("AdminNewsList");
        }

        public ActionResult DeleteNews(int id)
        {
            nm.NewsDelete(id);
            return RedirectToAction("AdminNewsList");
        }

        [HttpGet]
        public ActionResult UpdateNews(int id)
        {
            News news = nm.FindNews(id);
            NewsDataBaseEntities c = new NewsDataBaseEntities();
            List<SelectListItem> values = (from x in c.Categories.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.CategoryName,
                                               Value = x.id.ToString()
                                           }).ToList();

            List<SelectListItem> values2 = (from x in c.Authors.ToList()
                                            select new SelectListItem
                                            {
                                                Text = x.User.Name + " " + x.User.Surname,
                                                Value = x.id.ToString()
                                            }).ToList();
            ViewBag.values = values;
            ViewBag.values2 = values2;
            return View(news);
        }

        [HttpPost]
        public ActionResult UpdateNews(News p)
        {
            if (Request.Files != null && Request.Files.Count > 0)
            {
                var file = Request.Files[0];
                if (file.ContentLength > 0)
                {
                    var folder = Server.MapPath("~/images/newsuploads/");
                    var filename = Guid.NewGuid() + ".jpg";
                    file.SaveAs(Path.Combine(folder, filename));

                    var filePath = "~/images/newsuploads/" + filename;
                    p.Image = filePath;
                }

            }
            nm.NewsUpdate(p);
            return RedirectToAction("AdminNewsList");
        }

        public ActionResult GetCommentByNews(int id)
        {
            CommentManager cm = new CommentManager();
            var commentlist = cm.CommenByNews(id);
            return View(commentlist);
        }

        public ActionResult AuthorNewsList(int id)
        {
            var authornews = nm.GetNewsAuthorId(id);
            return View(authornews);
        }

        public ActionResult AuthorListFalse()
        {
            var authorstatus = autman.AuthorByStatusFalse();
            return View(authorstatus);
        }
        public ActionResult StatusChangeToTrue(int id)
        {
            autman.AuthorStatusChangeToTrue(id);
            return RedirectToAction("AdminAuthorList", "Author");
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Login", "Login");
        }
    }
}