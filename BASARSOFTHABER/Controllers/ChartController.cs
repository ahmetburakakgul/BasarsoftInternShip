using BASARSOFTHABER.Models.ChartClass;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BASARSOFTHABER.Controllers
{
    public class ChartController : Controller
    {
        // GET: Chart
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult VisualizeResult()
        {
            return Json(categorylist(), JsonRequestBehavior.AllowGet);
        }

        public List<Class1> categorylist()
        {
            List<Class1> c = new List<Class1>();
            c.Add(new Class1()
            {
                CategoryName = "Spor",
                NewsCount = 14
            });
            c.Add(new Class1()
            {
                CategoryName = "Ekonomi",
                NewsCount = 10
            });
            c.Add(new Class1()
            {
                CategoryName = "Siyaset",
                NewsCount = 20
            });
            return c;
        }


        public ActionResult VisualizeResult2()
        {
            return Json(NewsList(), JsonRequestBehavior.AllowGet);
        }

        public List<Class2> NewsList()
        {
            List<Class2> cs2 = new List<Class2>();
            using(var c = new NewsDataBaseEntities())
            {
                cs2 = c.News.Select(x => new Class2
                {
                    NewsName = x.NewsName,
                    Rating = (int)x.NewsRating
                }).ToList();
            }
            return cs2;
        }

        public ActionResult Chart1()
        {
            return View();
        }

        public ActionResult Chart2()
        {
            return View();
        }

        public ActionResult Chart3()
        {
            return View();
        }
    }
}