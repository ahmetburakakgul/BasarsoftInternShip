using BusinessLayer.Concrete;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BASARSOFTHABER.Controllers
{
    public class UserMemberController : BaseController
    {
        UserMemberManager usm = new UserMemberManager();


        [HttpGet]
        public new ActionResult Profile(string p)
        {
            p = (string)Session["UserMail"];
            var userprofilevalues = usm.GetUserByMail(p);
            return View(userprofilevalues);
        }

        [HttpGet]
        public ActionResult ProfileEdit(int id)
        {
            User user = usm.FindUser(id);
            return View(user);
        }


        [HttpPost]
        public ActionResult ProfileEdit(User p)
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
                    p.Image = filePath;
                }

            }
            p.RoleId = 3;
            usm.EditProfile(p);
            return RedirectToAction("Profile");
        }

        [HttpGet]
        public ActionResult ToBeAWriter(int id)
        {
            User user = usm.FindUser(id);
            NewsDataBaseEntities c = new NewsDataBaseEntities();
            List<SelectListItem> authorcategoryvalue = (from x in c.AuthorCategories.ToList()
                                                        select new SelectListItem
                                                        {
                                                            Text = x.CategoryName,
                                                            Value = x.id.ToString()
                                                        }).ToList();
            ViewBag.authorcat = authorcategoryvalue;
            return View(user);
        }

        [HttpPost]
        public ActionResult ToBeAWriter(User p,Author a,FormCollection form)
        {
            p.ShortAbout = Request.Form["ShortAbout"].ToString();
            usm.EditProf(p);
            int autcat = int.Parse(Request.Form["AuthorCategories"]);            
            a.UserId = int.Parse(form["id"]);
            a.IsAuthor = false;
            a.AuthorCategoryId = autcat;
            usm.UserAuthorAdd(a);
            return RedirectToAction("Profile");
        }
    }
}