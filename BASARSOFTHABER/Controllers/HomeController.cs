using BASARSOFTHABER.Models.HomeModel;
using BusinessLayer.Concrete;
using EntityLayer.Concrete;
using PagedList;
using PagedList.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BASARSOFTHABER.Controllers
{
    public class HomeController : BaseController
    {
        NewsManager nm = new NewsManager();

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index(int id = 0)
        {
            IQueryable<News> news = Context.News.OrderByDescending(x => x.PublicationDate).Where(x => x.IsDeleted == false || x.IsDeleted == null);
            Category category = null;
            if (id > 0)
            {
                category = Context.Categories.FirstOrDefault(x => x.id == id);
                var allcategories = GetChildCategories(category);
                allcategories.Add(category);

                var catInList = allcategories.Select(x => x.id).ToList();
                //Bir int nesnenin içindeki tüm kategorileri listeliyor
                news = news.Where(x => catInList.Contains(x.CategoryId));
            }
            var viewmodel = new HomeModel()
            {
                News = news.ToList(),
                Category = category
            };
            return View(viewmodel);
        }

        [AllowAnonymous]
        public ActionResult NewsByCategory(int id)
        {
            var newsbycategory = nm.GetNewsByCategory(id);
            return View(newsbycategory);
        }


        //public PartialViewResult NewsList(int? page)
        //{
        //    var newslist = nm.GetAll().ToPagedList(page ?? 1, 12);
        //    return PartialView(newslist);
        //}
        [AllowAnonymous]
        public PartialViewResult FeatureSlideNews()
        {
            var slidenews = nm.GetAll();
            return PartialView(slidenews);
        }
        [AllowAnonymous]
        public PartialViewResult FeatureNews()
        {
            var newstitle1 = nm.GetAll().OrderByDescending(z => z.id).Where(x => x.CategoryId == 1).Select(y => y.NewsName).FirstOrDefault();
            var newsımage1 = nm.GetAll().OrderByDescending(z => z.id).Where(x => x.CategoryId == 1).Select(y => y.Image).FirstOrDefault();
            var newscategory1 = nm.GetAll().OrderByDescending(z => z.id).Where(x => x.CategoryId == 1).Select(y => y.Category.CategoryName).FirstOrDefault();

            ViewBag.newstitle1 = newstitle1;
            ViewBag.newsımage1 = newsımage1;
            ViewBag.newscategory1 = newscategory1;

            var newstitle2 = nm.GetAll().OrderByDescending(z => z.id).Where(x => x.CategoryId == 2).Select(y => y.NewsName).FirstOrDefault();
            var newsımage2 = nm.GetAll().OrderByDescending(z => z.id).Where(x => x.CategoryId == 2).Select(y => y.Image).FirstOrDefault();
            var newscategory2 = nm.GetAll().OrderByDescending(z => z.id).Where(x => x.CategoryId == 2).Select(y => y.Category.CategoryName).FirstOrDefault();

            ViewBag.newstitle2 = newstitle2;
            ViewBag.newsımage2 = newsımage2;
            ViewBag.newscategory2 = newscategory2;

            var newstitle3 = nm.GetAll().OrderByDescending(z => z.id).Where(x => x.CategoryId == 3).Select(y => y.NewsName).FirstOrDefault();
            var newsımage3 = nm.GetAll().OrderByDescending(z => z.id).Where(x => x.CategoryId == 3).Select(y => y.Image).FirstOrDefault();
            var newscategory3 = nm.GetAll().OrderByDescending(z => z.id).Where(x => x.CategoryId == 3).Select(y => y.Category.CategoryName).FirstOrDefault();

            ViewBag.newstitle3 = newstitle3;
            ViewBag.newsımage3 = newsımage3;
            ViewBag.newscategory3 = newscategory3;

            return PartialView();
        }
        [AllowAnonymous]
        public ActionResult NewsDetails()
        {
            return View();
        }
        [AllowAnonymous]
        public PartialViewResult NewsCover(int id)
        {
            var NewsDetailList = nm.GetNewsById(id);
            return PartialView(NewsDetailList);
        }
        [AllowAnonymous]
        public PartialViewResult NewsReadAll(int id)
        {
            var NewsDetailList = nm.GetNewsById(id);
            return PartialView(NewsDetailList);
        }
    }
}