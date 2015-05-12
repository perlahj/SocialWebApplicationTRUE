using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SozialWebApplication.Models.ViewModels;
using SozialWebApplication.ViewModel;
using SozialWebApplication.Services;
using SozialWebApplication.Models;
using SozialWebApplication.Models.Entities;

namespace SozialWebApplication.Controllers
{
	public class HomeController : Controller
	{
        private PostService ps = new PostService();
		private GroupService gs = new GroupService();
		private NameCardViewModel nameCardVM = new NameCardViewModel();
        private GroupViewModel GroupVM = new GroupViewModel();
		
		public ActionResult Banner()
		{
			return View();
		}

        public ActionResult NewsfeedGroups(int? id)
        {
            return PartialView("~/Views/Home/NewsfeedGroups.cshtml", GroupVM);
        }

        [NonAction]
        public List<Post> GroupPosts()
        {
            var name = (from i in ps.AddNewPost select i).ToList();
            return name;
        }
        
        [HttpGet]
        public ActionResult GroupPost()
        {
            GroupViewModel model = new GroupViewModel();
            model.Group = gs.GetGroupFromId(2);

            //ps.AddNewPost("Palli", 2, "Takk fyrir seinast sæta");

            return PartialView("~/Views/Home/GroupPost.cshtml", model);
        }
        
        /*[HttpPost]
        public ActionResult GroupPost(FormCollection collection)
        {
            
            int groupId = collection.Get("groupId");
            string postBody = collection.Get("postbody");
            string userName = User.Identity.Name;
            ps.AddNewPost(userName, groupId, postBody);

            private GroupViewModel model = new GroupViewModel();
            model.Group = gs.GetGroupFromId(groupId);

            return PartialView("~/Views/Home/PostGroups.cshtml", model);
        }*/

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

			/*gs.AddNewGroup("Strjál stærðfræði 2 vor 2015");
			gs.AddNewGroup("Gagnaskipan vor 2015");
			gs.AddNewGroup("Tvíund");
			gs.AddUserToGroup(1, User.Identity.Name);
			gs.AddUserToGroup(2, User.Identity.Name);
			gs.AddUserToGroup(3, User.Identity.Name);*/
			gs.AddNewGroup("Study Buddies");
			gs.AddNewGroup("Verklegt 2 Hópur 38 V2015");
			
			nameCardVM.AllUserGroups = gs.GetAllGroupsForUser(User.Identity.Name);
			nameCardVM.AllGroups = gs.GetAllGroups();
			nameCardVM.SearchResultsGroups = gs.SearchAllGroups("");
            return PartialView("~/Views/Home/SearchGroups.cshtml", nameCardVM);
		}

		[HttpPost]
		public ActionResult SearchGroups(FormCollection collection)
		{
			nameCardVM.AllUserGroups = gs.GetAllGroupsForUser(User.Identity.Name);
			nameCardVM.AllGroups = gs.GetAllGroups();
			nameCardVM.SearchResultsGroups = gs.SearchAllGroups(collection.Get("search"));
            return PartialView("~/Views/Home/SearchGroups.cshtml", nameCardVM);
		} 

        public ActionResult CheckMatch()
        {
            return View();
        }
	}
}