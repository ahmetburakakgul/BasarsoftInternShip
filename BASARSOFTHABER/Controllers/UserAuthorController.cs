using BusinessLayer.Concrete;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BASARSOFTHABER.Controllers
{
    public class UserAuthorController : Controller
    {
        UserProfileManager usm = new UserProfileManager();
        NewsManager nm = new NewsManager();
        public ActionResult Index(string p)
        {
            p = (string)Session["UserMail"];
            var userprofilevalues = usm.GetUserByMail(p);
            return View(userprofilevalues);
        }

        [HttpGet]
        public ActionResult UpdateUserProfile(int id)
        {
            Author user = usm.FindAuthor(id);
            return View(user);
        }

        [HttpPost]
        public ActionResult UpdateUserProfile(Author p)
        {
            if (Request.Files != null && Request.Files.Count > 0)
            {
                var file = Request.Files[0];
                if (file.ContentLength > 0)
                {
                    var folder = Server.MapPath("~/images/profileuploads/");
                    var filename = Guid.NewGuid() + ".jpg";
                    file.SaveAs(Path.Combine(folder, filename));

                    var filePath = "~/images/profileuploads/" + filename;
                    p.User.Image = filePath;
                }

            }
            usm.EditAuthor(p);
            return RedirectToAction("Index");
        }

        public ActionResult NewsList(string p)
        {
            p = (string)Session["UserMail"];
            NewsDataBaseEntities c = new NewsDataBaseEntities();
            int id = c.Authors.Where(x => x.User.EmailAddress == p).Select(y => y.id).FirstOrDefault();
            var news = usm.GetNewsByAuthor(id);
            return View(news);
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

            string p = (string)Session["UserMail"];
            int authorid = c.Authors.Where(x => x.User.EmailAddress == p).Select(y => y.id).FirstOrDefault();
            List<SelectListItem> values2 = (from x in c.Authors.Where(x => x.id == authorid).ToList()
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
            return RedirectToAction("NewsList");
        }

        [HttpGet]
        public ActionResult AddNewNews()
        {
            NewsDataBaseEntities c = new NewsDataBaseEntities();
            List<SelectListItem> values = (from x in c.Categories.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.CategoryName,
                                               Value = x.id.ToString()
                                           }).ToList();
            string p = (string)Session["UserMail"];
            int authorid = c.Authors.Where(x => x.User.EmailAddress == p).Select(y => y.id).FirstOrDefault();
            List<SelectListItem> values2 = (from x in c.Authors.Where(x => x.id == authorid)
                                            select new SelectListItem
                                            {
                                                Text = x.User.Name + " " + x.User.Surname,
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
            nm.NewsAddBl(p);
            return RedirectToAction("NewsList");
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Login", "Login");
        }
    }
}