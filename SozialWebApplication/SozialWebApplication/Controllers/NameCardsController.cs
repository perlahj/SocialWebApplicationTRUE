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
		private NameCardViewModel nameCardVM = new NameCardViewModel();
		
		public ActionResult OwnNameCard()
        {
			nameCardVM.userWithId = us.GetUserByUserName(User.Identity.Name);
			
			return PartialView("~/Views/NameCards/OwnNameCard.cshtml", nameCardVM);
        }

        public ActionResult OthersNameCard(string id)
        {
			NameCardViewModel model = new NameCardViewModel();
			model.userWithId = us.GetUserById(id);
			return PartialView("~/Views/NameCards/OthersNameCard.cshtml", model);
        }

		[HttpPost]
		public ActionResult OthersNameCard(string id, FormCollection collection)
		{
			string othersUserName = collection.Get("hidden-othersUserName");
			string action = collection.Get("hidden-follow-unfollow");
			if(action == "follow")
			{
				us.AddNewFollow(User.Identity.Name, othersUserName);
			}
			else if(action == "unfollow")
			{
				us.RemoveFollow(User.Identity.Name, othersUserName);
			}

			NameCardViewModel model = new NameCardViewModel();
			model.userWithId = us.GetUserById(id);
			return PartialView("~/Views/NameCards/OthersNameCard.cshtml", model);
		}

		/*[HttpPost]
		public ActionResult RemoveFollow(string id, FormCollection collection)
		{
			string othersUserName = collection.Get("hidden-OthersUserName");
			us.RemoveFollow(User.Identity.Name, othersUserName);
			NameCardViewModel model = new NameCardViewModel();
			model.userWithId = us.GetUserById(id);
			return PartialView("~/Views/NameCards/OthersNameCard.cshtml", model);
		} */

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
			nameCardVM.AllFollowing = us.GetAllFollowing(User.Identity.Name);
			nameCardVM.SearchResultsUsers = us.SearchAllUsers("");
			return View(nameCardVM);
		}

		[HttpPost]
		public ActionResult Search(FormCollection collection)
		{
			nameCardVM.AllUsers = us.GetAllUsers();
			nameCardVM.AllFollowing = us.GetAllFollowing(User.Identity.Name);
			nameCardVM.SearchResultsUsers = us.SearchAllUsers(collection.Get("search"));
			return View(nameCardVM);
		}
		   
	}

}