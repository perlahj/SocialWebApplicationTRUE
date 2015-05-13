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
			
			return View(nameCardVM);
        }

        public ActionResult OthersNameCard(string id)
        {
			NameCardViewModel model = new NameCardViewModel();
			model.userWithId = us.GetUserById(id);
			return View(model);
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
			return View(model);
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

            return View(nameCardVM);
		}

		[HttpPost]
		public ActionResult EditNameCard(FormCollection collection)
        {
			ApplicationUser currentUser = us.GetUserByUserName(User.Identity.Name);
			
			string fullName = collection.Get("input-name");
			if(string.IsNullOrEmpty(fullName))
			{
				fullName = currentUser.FullName;
			}
			string lineOfStudy = collection.Get("input-los");
			if (String.IsNullOrEmpty(lineOfStudy))
			{
				lineOfStudy = currentUser.LineOfStudy;
			}
			string email = collection.Get("input-email");
			if (String.IsNullOrEmpty(email))
			{
				email = currentUser.Email;
			}
			
			us.ChangeFullName(User.Identity.Name, fullName);
			us.ChangeLineOfStudy(User.Identity.Name, lineOfStudy);
			us.ChangeEmail(User.Identity.Name, email);

            nameCardVM.userWithId = us.GetUserByUserName(User.Identity.Name);

            return View(nameCardVM);
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