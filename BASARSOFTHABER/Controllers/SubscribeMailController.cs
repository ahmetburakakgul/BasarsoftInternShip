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
    public class SubscribeMailController : BaseController
    {
        SubscribeMailManager sm = new SubscribeMailManager();

        [HttpGet]
        public PartialViewResult AddMail()
        {
            return PartialView();
        }

        [HttpPost]
        public PartialViewResult AddMail(SubscribeMail p)
        {
            if (Request.Form["subscribe"]!=null)
            {
                sm.BLAdd(p);                
            }
            return PartialView();

        }
    }
}