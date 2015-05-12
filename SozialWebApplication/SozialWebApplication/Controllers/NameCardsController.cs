using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SozialWebApplication.Services;
using SozialWebApplication.Models;
using SozialWebApplication.Models.ViewModels;
using SozialWebApplication.ViewModel;

namespace SozialWebApplication.Controllers
{
    public class NameCardsController : Controller
    {

		private UserService us = new UserService();
		//private GroupService gs = new GroupService();
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

        public ActionResult OthersNameCard(string userName)
        {
			nameCardVM.userWithId = us.GetUserByUserName(userName);
			
			return View();
        }


		[HttpGet]
		public ActionResult EditNameCard()
		{
			nameCardVM.userWithId = us.GetUserByUserName(User.Identity.Name);

            return PartialView("~/Views/NameCards/EditNameCard.cshtml", nameCardVM);
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

            return PartialView("~/Views/NameCards/EditNameCard.cshtml", nameCardVM);
        }

		public ActionResult Search()
		{
			nameCardVM.AllUsers = us.GetAllUsers();
			nameCardVM.SearchResultsUsers = us.SearchAllUsers("");
			return PartialView("~/Views/NameCards/Search.cshtml", nameCardVM);
		}

		[HttpPost]
		public ActionResult Search(FormCollection collection)
		{
			nameCardVM.AllUsers = us.GetAllUsers();
			nameCardVM.SearchResultsUsers = us.SearchAllUsers(collection.Get("search"));
            return PartialView("~/Views/NameCards/Search.cshtml", nameCardVM);
		}

		/*public ActionResult SearchGroups()
		{
			nameCardVM.AllUserGroups = gs.GetAllGroupsForUser(User.Identity.Name);
			//nameCardVM.SearchResultsGroups = gs.SearchAllGroups("");
			return View(nameCardVM);
		}

		/*[HttpPost]
		public ActionResult SearchGroups(FormCollection collection)
		{
			nameCardVM.AllUsers = us.GetAllUsers();
			nameCardVM.SearchResultsUsers = us.SearchAllUsers(collection.Get("search"));
			return View(nameCardVM);
		}  */
		   
	}

}