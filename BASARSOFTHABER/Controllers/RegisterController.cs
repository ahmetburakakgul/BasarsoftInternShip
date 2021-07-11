using BASARSOFTHABER.Models.RegisterModel;
using BusinessLayer.Concrete;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BASARSOFTHABER.Controllers
{
    [AllowAnonymous]
    public class RegisterController : BaseController
    {
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(RegisterModel p)
        {
            try
            {
                if (p.User.Password != p.RePasword)
                {
                    throw new Exception("Şifreler Aynı Değil");
                }
                if (Context.Users.Any(x => x.EmailAddress == p.User.EmailAddress))
                {
                    throw new Exception("Bu E-posta Zaten Kayıtlı");
                }
                p.User.About = null;
                p.User.ShortAbout = null;
                p.User.Image = "https://bootdey.com/img/Content/avatar/avatar7.png";
                p.User.RoleId = 3;
                Context.Users.Add(p.User);
                Context.SaveChanges();
                return RedirectToAction("Login", "Login");

            }
            catch (Exception ex)
            {
                ViewBag.ReError = ex.Message;
                return View();
            }
        }
    }
}