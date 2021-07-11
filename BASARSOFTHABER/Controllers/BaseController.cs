using BusinessLayer.Concrete;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BASARSOFTHABER.Controllers
{
    public class BaseController : Controller
    {

        protected NewsDataBaseEntities Context { get; private set; }
        public BaseController()
        {
            Context = new NewsDataBaseEntities();
            ViewBag.MenuCategories = Context.Categories.Where(x => x.ParentId == null).ToList();
        }

        protected User CurrentUser()
        {
            if (Session["UserMail"] == null) return null;
            return (User)Session["UserMail"];
        }
        protected int CurrentUserId()
        {
            if (Session["UserMail"] == null) return 0;
            return ((User)Session["UserMail"]).id;
        }
        protected bool IsLogon()
        {
            if (Session["UserMail"] == null)
                return false;
            else
                return true;

        }

        protected List<Category> GetChildCategories(Category cat)
        {
            var result = new List<Category>();

            result.AddRange(cat.Category1);
            foreach (var item in cat.Category1)//alt kategorilerinde altını almak için dönüyorum.
            {
                var list = GetChildCategories(item);
                result.AddRange(list);
            }
            return result;
        }
    }
}