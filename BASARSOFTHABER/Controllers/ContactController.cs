using BusinessLayer.Concrete;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BASARSOFTHABER.Controllers
{
    public class ContactController : BaseController
    {
        // GET: Contact
        ContactManager cm = new ContactManager();
        //public ActionResult Index()
        //{
        //    return View();
        //}

        [HttpGet]
        [AllowAnonymous]
        public ActionResult SendMessage()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult SendMessage(Contact p)
        {
            if (Request.Form["contact"] != null)
            {
                p.MessageDate = DateTime.Now;
                cm.ContactAdd(p);
            }
            return View();
        }

        public ActionResult SendBox()
        {
            var messagelist = cm.GetAll();
            return View(messagelist);
        }

        public ActionResult MessageDetails(int id)
        {
            Contact contact = cm.GetContactDetails(id);
            return View(contact);
        }
    }
}