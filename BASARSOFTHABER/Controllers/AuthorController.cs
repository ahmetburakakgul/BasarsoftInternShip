using BusinessLayer.Concrete;
using EntityLayer.Concrete;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BASARSOFTHABER.Controllers
{
    public class AuthorController : BaseController
    {
        NewsManager nm = new NewsManager();
        AuthorManager authman = new AuthorManager();

        [AllowAnonymous]
        public ActionResult Index()
        {
            var authmanlist = authman.GetAll();
            return View(authmanlist);
        }

        [AllowAnonymous]
        public PartialViewResult AuthorAbout(int id)
        {
            var authordetail = nm.GetNewsById(id);
            return PartialView(authordetail);
        }

        [AllowAnonymous]
        public PartialViewResult AuthorPopularPost(int id)
        {
            var authorid = nm.GetAll().Where(x => x.id == id).Select(y => y.AuthorId).FirstOrDefault();

            var authornews = nm.GetNewsAuthorId(authorid);
            return PartialView(authornews);
        }

        public ActionResult AdminAuthorList(int? page)
        {
            var authorlist = authman.GetAll().ToPagedList(page ?? 1, 6);
            return View(authorlist);
        }

        [HttpGet]
        public ActionResult AddAuthor()
        {
            NewsDataBaseEntities c = new NewsDataBaseEntities();
            List<SelectListItem> authorcategoryvalue = (from x in c.AuthorCategories.ToList()
                                                        select new SelectListItem
                                                        {
                                                            Text = x.CategoryName,
                                                            Value = x.id.ToString()
                                                        }).ToList();
            List<SelectListItem> authorrolevalue = (from x in c.Roles.ToList()
                                                    select new SelectListItem
                                                    {
                                                        Text = x.Roles,
                                                        Value = x.id.ToString()
                                                    }).ToList();

            ViewBag.authorcategoryvalue = authorcategoryvalue;
            ViewBag.authorrolevalue = authorrolevalue;

            return View();
        }

        [HttpPost]
        public ActionResult AddAuthor(Author p)
        {
            if (Request.Files != null && Request.Files.Count > 0)
            {
                var file = Request.Files[0];
                if (file.ContentLength > 0)
                {
                    var folder = Server.MapPath("~/images/authoruploads/");
                    var filename = Guid.NewGuid() + ".jpg";
                    file.SaveAs(Path.Combine(folder, filename));

                    var filePath = "~/images/authoruploads/" + filename;
                    p.User.Image = filePath;
                }

            }
            authman.AuthorAdd(p);
            return RedirectToAction("AdminAuthorList");
        }

        [HttpGet]
        public ActionResult AuthorEdit(int id)
        {
            Author author = authman.FindAuthor(id);
            NewsDataBaseEntities c = new NewsDataBaseEntities();
            List<SelectListItem> authorcategoryvalue1 = (from x in c.AuthorCategories.ToList()
                                                         select new SelectListItem
                                                         {
                                                             Text = x.CategoryName,
                                                             Value = x.id.ToString()
                                                         }).ToList();
            List<SelectListItem> authorrolevalue1 = (from x in c.Roles.ToList()
                                                     select new SelectListItem
                                                     {
                                                         Text = x.Roles,
                                                         Value = x.id.ToString()
                                                     }).ToList();

            ViewBag.authorcategoryvalue1 = authorcategoryvalue1;
            ViewBag.authorrolevalue1 = authorrolevalue1;

            return View(author);
        }

        [HttpPost]
        public ActionResult AuthorEdit(Author p)
        {
            if (Request.Files != null && Request.Files.Count > 0)
            {
                var file = Request.Files[0];
                if (file.ContentLength > 0)
                {
                    var folder = Server.MapPath("~/images/authoruploads/");
                    var filename = Guid.NewGuid() + ".jpg";
                    file.SaveAs(Path.Combine(folder, filename));

                    var filePath = "~/images/authoruploads/" + filename;
                    p.User.Image = filePath;
                }

            }
            int role = int.Parse(Request.Form["RoleId"]);
            if (role == 3)
            {
                int id = int.Parse(Request.Form["id"]);
                UserMemberManager userMember = new UserMemberManager();
                p.IsAuthor = false;
                p.User.RoleId = role;
                authman.EditAuthor(p);
                userMember.AuthorDelete(id);
                return RedirectToAction("AdminAuthorList");
            }
            else if (role==1)
            {
                p.User.RoleId = role;
                authman.EditAuthor(p);
            }
            else
            {
                p.User.RoleId = role;
                authman.EditAuthor(p);
            }
            authman.EditAuthor(p);
            return RedirectToAction("AdminAuthorList");
        }
    }
}