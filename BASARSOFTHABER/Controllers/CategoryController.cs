using BusinessLayer.Concrete;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BASARSOFTHABER.Controllers
{
    public class CategoryController : Controller
    {
        NewsDataBaseEntities Context = new NewsDataBaseEntities();
        CategoryManager cm = new CategoryManager();
        public ActionResult Index()
        {
            //var categories = Context.Categories.Where(x => x.IsDeleted == false || x.IsDeleted == null).ToList();
            var categoryList = cm.GetAll();
            return View(categoryList);
        }

        [HttpGet]
        public ActionResult Edit(int id = 0)
        {
            var cat = Context.Categories.FirstOrDefault(x => x.id == id);
            var categories = Context.Categories.Select(x => new SelectListItem()
            {
                Text = x.CategoryName,
                Value = x.id.ToString()
            }).ToList();
            categories.Add(new SelectListItem()
            {
                Value = "0",
                Text = "Ana Kategori",
                Selected = true
            });
            ViewBag.Categories = categories;
            return View(cat);
        }

        [HttpPost]
        public ActionResult Edit(Category category)
        {
            if (category.id > 0)
            {
                var cat = Context.Categories.FirstOrDefault(x => x.id == category.id);
                cat.Description = category.Description;
                cat.CategoryName = category.CategoryName;
                cat.EditDate = DateTime.Now;
                cat.IsDeleted = false;
                if (category.ParentId > 0)
                    cat.ParentId = category.Category2.ParentId;
                else
                    cat.ParentId = null;
            }
            else
            {
                category.CreationDate = DateTime.Now;
                category.IsDeleted = false;
                if (category.ParentId == 0)
                    category.ParentId = null;
                Context.Entry(category).State = System.Data.Entity.EntityState.Added;
            }
            Context.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DeleteCategory(int id)
        {
            cm.CategoryDelete(id);
            return RedirectToAction("Index");
        }


        public ActionResult AuthorCategoryIndex()
        {
            var authorcat = cm.ListAllAutCat();
            return View(authorcat);
        }

        [HttpGet]
        public ActionResult AddAuthorCategory()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddAuthorCategory(AuthorCategory p)
        {
            p.CreationDate = DateTime.Now;
            cm.AuthorCategoryAdd(p);
            return RedirectToAction("AuthorCategoryIndex");
        }

        [HttpGet]
        public ActionResult EditAuthorCategory(int id)
        {
            var authoreditcat = cm.FindAuthorCat(id);
            return View(authoreditcat);
        }


        [HttpPost]
        public ActionResult EditAuthorCategory(AuthorCategory authorCategory)
        {
            authorCategory.EditDate = DateTime.Now;
            cm.EditAuthorCategory(authorCategory);
            return RedirectToAction("AuthorCategoryIndex");
        }

        public ActionResult DeleteCategoryAuthor(int id)
        {
            cm.AuthorCategoryDelete(id);
            return RedirectToAction("AuthorCategoryIndex");
        }

    }
}