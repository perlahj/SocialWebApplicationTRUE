using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SozialWebApplication.Controllers
{
    public class NameCardsController : Controller
    {

        public ActionResult OwnNameCard()
        {
            //[ChildActionOnlyAttribute]
            
          /*  var ownnamecard = User.Identity.Name*/
            /*return PartialView();*/
            //return PartialView("OwnNameCard");
            return View();
        }

        public ActionResult OthersNameCard()
        {
            return View();
        }

        public ActionResult EditNameCard()
        {
            return View();
        }

        public ActionResult Search()
        {
            return View();
        }

	}

}