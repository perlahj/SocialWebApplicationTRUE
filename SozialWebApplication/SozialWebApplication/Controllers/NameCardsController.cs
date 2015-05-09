using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SozialWebApplication.Services;
using SozialWebApplication.Models;
using SozialWebApplication.Models.ViewModels;

namespace SozialWebApplication.Controllers
{
    public class NameCardsController : Controller
    {

		private UserService us = new UserService();
		
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

		public ActionResult EditNameCard(FormCollection collection)
        {
			NameCardViewModel nameCardVM = new NameCardViewModel();
			//nameCardVM.userWithId = UserService.Instance.GetUserByUserName(User.Identity.Name);
			nameCardVM.userWithId = us.GetUserByUserName(User.Identity.Name);
			
			string fullName = collection.Get("input-name");
			string lineOfStudy = collection.Get("input-los");
			us.ChangeFullName(User.Identity.Name, fullName);
			us.ChangeLineOfStudy(User.Identity.Name, lineOfStudy);

			return View(nameCardVM);
        }


        public ActionResult Search()
        {
            return View();
        }

	}

}