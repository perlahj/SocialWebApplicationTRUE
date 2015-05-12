using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SozialWebApplication.Models.ViewModels;
using SozialWebApplication.ViewModel;
using SozialWebApplication.Services;

namespace SozialWebApplication.Controllers
{
	public class HomeController : Controller
	{
		private GroupService gs = new GroupService();
		private NameCardViewModel nameCardVM = new NameCardViewModel();
        private GroupViewModel GroupVM = new GroupViewModel();
        private UserService us = new UserService();
		
		public ActionResult Banner()
		{
			return View();
		}

        public ActionResult NewsfeedGroups(int? id)
        {
            return PartialView("~/Views/Home/NewsfeedGroups.cshtml", GroupVM);
        }

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}

		public ActionResult SearchGroups()
		{			
			nameCardVM.AllUserGroups = gs.GetAllGroupsForUser(User.Identity.Name);
			nameCardVM.AllGroups = gs.GetAllGroups();
			nameCardVM.SearchResultsGroups = gs.SearchAllGroups("");
            return PartialView("~/Views/Home/SearchGroups.cshtml", nameCardVM);
		}

		[HttpPost]
		public ActionResult SearchGroups(int id, FormCollection collection)
		{
			nameCardVM.AllUserGroups = gs.GetAllGroupsForUser(User.Identity.Name);
			nameCardVM.AllGroups = gs.GetAllGroups();
			nameCardVM.SearchResultsGroups = gs.SearchAllGroups(collection.Get("search"));
            return PartialView("~/Views/Home/SearchGroups.cshtml", nameCardVM);
		} 

        public ActionResult CheckMatch()
        {
            // senda model inn i rett view og tha er haegt ad vinna med gognin
            nameCardVM.AllMatches = us.GetAllDoubleMatches(User.Identity.Name);

            return PartialView("~/Views/Home/CheckMatch.cshtml", nameCardVM);
        }
	}
}