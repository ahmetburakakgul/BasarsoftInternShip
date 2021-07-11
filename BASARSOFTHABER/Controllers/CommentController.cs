using BusinessLayer.Concrete;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BASARSOFTHABER.Controllers
{
    public class CommentController : BaseController
    {
        CommentManager cm = new CommentManager();

        [AllowAnonymous]
        public PartialViewResult CommentList(int id)
        {
            var commentlist = cm.CommenByNews(id);
            return PartialView(commentlist);
        }

        [HttpGet]
        [AllowAnonymous]
        public PartialViewResult LeaveComment(int id)
        {
            ViewBag.id = id;
            return PartialView();
        }

        [HttpPost]
        [AllowAnonymous]
        public PartialViewResult LeaveComment(Comment c)
        {
            if (Request.Form["leave"] != null)
            {
                cm.CommentAdd(c);
            }            
            return PartialView();
        }


        public ActionResult AdminCommentListTrue()
        {
            var commentlist = cm.CommentByStatusTrue();
            return View(commentlist);
        }

        public ActionResult AdminCommentListFalse()
        {
            var commentlist = cm.CommentByStatusFalse();
            return View(commentlist);
        }

        public ActionResult StatusChangeToFalse(int id)
        {
            cm.CommentStatusChangeToFalse(id);
            return RedirectToAction("AdminCommentListTrue");
        }

        public ActionResult StatusChangeToTrue(int id)
        {
            cm.CommentStatusChangeToTrue(id);
            return RedirectToAction("AdminCommentListFalse");
        }
    }
}