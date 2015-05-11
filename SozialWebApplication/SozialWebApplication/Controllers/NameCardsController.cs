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

			nameCardVM.userWithId = us.GetUserByUserName(User.Identity.Name);
			
			return PartialView("~/Views/NameCards/OwnNameCard.cshtml", nameCardVM);
        }

        public ActionResult OthersNameCard(string otherUser)
        {
			nameCardVM.userWithId = us.GetUserByUserName(otherUser);
			
			return View(nameCardVM);
			//return PartialView("~/Views/NameCards/OthersNameCard.cshtml", nameCardVM);
        }


		[HttpGet]
		public ActionResult EditNameCard()
		{
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

		public ActionResult Search()
		{
			nameCardVM.AllUsers = us.GetAllUsers();
			nameCardVM.SearchResultsUsers = us.SearchAllUsers("");
			return View(nameCardVM);
		}

		[HttpPost]
		public ActionResult Search(FormCollection collection)
		{
			nameCardVM.AllUsers = us.GetAllUsers();
			nameCardVM.SearchResultsUsers = us.SearchAllUsers(collection.Get("search"));
			return View(nameCardVM);
		} 
		   
	}

}