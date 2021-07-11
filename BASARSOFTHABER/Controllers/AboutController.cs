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
    [AllowAnonymous]
    public class AboutController : BaseController
    {
        AboutManager abm = new AboutManager();
        // GET: About
        public ActionResult Index()
        {
            var aboutcontent = abm.GetAll();
            return View(aboutcontent);
        }

        public PartialViewResult Footer()
        {
            return PartialView();
        }

        [HttpGet]
        public ActionResult UpdateAboutList()
        {
            var aboutlist = abm.GetAll();
            return View(aboutlist);
        }

        [HttpPost]
        public ActionResult UpdateAbout(About p)
        {
            if (Request.Files != null && Request.Files.Count > 0)
            {
                var file = Request.Files[0];
                if (file.ContentLength > 0)
                {
                    var folder = Server.MapPath("~/images/aboutuploads/");
                    var filename = Guid.NewGuid() + ".jpg";
                    file.SaveAs(Path.Combine(folder, filename));

                    var filePath = "~/images/aboutuploads/" + filename;
                    p.AboutImage1 = filePath;
                }

            }
            if (Request.Files != null && Request.Files.Count > 0)
            {
                var file = Request.Files[1];
                if (file.ContentLength > 0)
                {
                    var folder = Server.MapPath("~/images/aboutuploads/");
                    var filename = Guid.NewGuid() + ".jpg";
                    file.SaveAs(Path.Combine(folder, filename));

                    var filePath = "~/images/aboutuploads/" + filename;
                    p.AboutImage2 = filePath;
                }

            }
            abm.AboutUpdate(p);
            return RedirectToAction("UpdateAboutList");
        }
    }
}