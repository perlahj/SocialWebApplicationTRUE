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
		private NameCardViewModel nameCardVM = new NameCardViewModel();
		
		public ActionResult OwnNameCard()
        {
            //[ChildActionOnlyAttribute]
            
          /*  var ownnamecard = User.Identity.Name*/
            /*return PartialView();*/
            //return PartialView("OwnNameCard");

			//NameCardViewModel nameCardVM = new NameCardViewModel();
			nameCardVM.userWithId = us.GetUserByUserName(User.Identity.Name);
			
			return PartialView("~/Views/NameCards/OwnNameCard.cshtml", nameCardVM);
        }

        public ActionResult OthersNameCard()
        {
            return View();
        }


		[HttpGet]
		public ActionResult EditNameCard()
		{
			//NameCardViewModel nameCardVM = new NameCardViewModel();
			//nameCardVM.userWithId = UserService.Instance.GetUserByUserName(User.Identity.Name);
			nameCardVM.userWithId = us.GetUserByUserName(User.Identity.Name);

			return View(nameCardVM);
		}
		[HttpPost]
		public ActionResult EditNameCard(FormCollection collection)
        {
			
			nameCardVM.userWithId = us.GetUserByUserName(User.Identity.Name);
			
			string fullName = collection.Get("input-name");
			string lineOfStudy = collection.Get("input-los");
			string email = collection.Get("input-email");
			
			us.ChangeFullName(User.Identity.Name, fullName);
			us.ChangeLineOfStudy(User.Identity.Name, lineOfStudy);
			us.ChangeEmail(User.Identity.Name, email);

			return View(nameCardVM);
        }

		public ActionResult Search(FormCollection collection)
        {
			nameCardVM.AllUsers = us.GetAllUsers();
			nameCardVM.SearchResultsUsers = us.SearchAllUsers(collection.Get(""));
			return View(nameCardVM);
        }

		/*[HttpPost]
		public ActionResult SearchPost(FormCollection collection)
		{
			nameCardVM.SearchResultsUsers = us.SearchAllUsers(collection.Get("search"));
			return View(nameCardVM);
		}*/ 
		   
	}

}