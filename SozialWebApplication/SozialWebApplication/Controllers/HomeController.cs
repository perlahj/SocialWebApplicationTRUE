using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SozialWebApplication.Services;
using SozialWebApplication.Models;
using SozialWebApplication.Models.Entities;
using SozialWebApplication.ViewModel;

namespace SozialWebApplication.Controllers
{
	public class HomeController : Controller
	{
		
		public ActionResult Index()
		{
			return View();
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

        public ActionResult Group(string? s)
        {
            GroupViewModel model = new GroupViewModel();

            // ATH: Smida fall sem saekir allt
            //Group g = Initialize(s);
            
            model.GroupName = g.GroupName;
            
            // smida fall sem saekir post listann
            //model.GroupPost = g.GroupPost;

            return View(model);
        }

        public ActionResult OtherUserProfile(string? s)
        {
            OtherUserProfileViewModel model = new OtherUserProfileViewModel();
            
            // Smida fall sem saekir allar uppl sem tharf her
            //ApplicationUser u = Initialize(s);
            model.FullName = u.FullName;
            model.LineOfStudy = u.LineOfStudy;
            
            model.Email = u.Email;

            return View(model);
        }
        
        public ActionResult UserProfile(string? s)
        {
            UserProfileViewModel model = new UserProfileViewModel();
            
            // Smida fall sem saekir allar uppl
            //ApplicationUser u = Initialize(s);
            model.FullName = u.FullName;
            model.LineOfStudy = u.LineOfStudy;

            model.Email = u.Email;

            return View(model);
        }

        public ActionResult Comment(string? s)
        {
            CommentViewModel model = new CommentViewModel();
            
            // Smida fall sem saekir allar uppl sem tharf
            //Comment c = Initialize(s);
            model.UserName = c.UserName;
            model.Body = c.Body;

            return View(model);
        }
	}
}