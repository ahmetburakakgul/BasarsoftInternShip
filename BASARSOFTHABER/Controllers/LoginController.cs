using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BASARSOFTHABER.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private NewsDataBaseEntities c = new NewsDataBaseEntities();
        // GET: Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(User user)
        {
            var userinfo = c.Users.FirstOrDefault(x => x.Password == user.Password && x.EmailAddress == user.EmailAddress);
            if (userinfo!=null && userinfo.Role.Roles.Equals("Yazar"))
            {
                FormsAuthentication.SetAuthCookie(userinfo.EmailAddress, false);
                Session["UserMail"] = userinfo.EmailAddress.ToString();
                Session["LogonUser"] = userinfo;
                return RedirectToAction("NewsList", "UserAuthor");
            }
            else if (userinfo != null && userinfo.Role.Roles.Equals("Admin"))
            {
                FormsAuthentication.SetAuthCookie(userinfo.EmailAddress, false);
                Session["UserMail"] = userinfo.EmailAddress.ToString();
                Session["LogonUser"] = userinfo;
                return RedirectToAction("AdminNewsList", "Admin");
            }
            else if (userinfo != null && userinfo.Role.Roles.Equals("Üye"))
            {
                FormsAuthentication.SetAuthCookie(userinfo.EmailAddress, false);
                Session["UserMail"] = userinfo.EmailAddress.ToString();
                Session["LogonUser"] = userinfo;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.ReError = "Kullanıcı Bilgileri Yanlış";
                return View();
            }
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
    }
}