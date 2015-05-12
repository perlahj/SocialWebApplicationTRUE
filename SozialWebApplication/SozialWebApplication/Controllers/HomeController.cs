using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SozialWebApplication.Models.ViewModels;
using SozialWebApplication.Services;

namespace SozialWebApplication.Controllers
{
	public class HomeController : Controller
	{
		private GroupService gs = new GroupService();
		private PostService ps = new PostService();
		private NameCardViewModel nameCardVM = new NameCardViewModel();
        private GroupViewModel GroupVM = new GroupViewModel();
		
		public ActionResult Banner()
		{
			return View();
		}

        public ActionResult NewsfeedGroups(int id)
        {
			GroupViewModel groupVM = new GroupViewModel();
			groupVM.GroupWithId = gs.GetGroupById(id);
			groupVM.GroupPosts = ps.GetLatestPostsForGroup(id);
			return PartialView("~/Views/Home/NewsfeedGroups.cshtml", groupVM);
        }

		[HttpPost]
		public ActionResult NewsfeedGroups(int id, FormCollection collection)
		{
			string postBody = collection.Get("post-input");
			ps.AddNewPost(User.Identity.Name, id, postBody);

			GroupViewModel groupVM = new GroupViewModel();
			groupVM.GroupWithId = gs.GetGroupById(id);
			groupVM.GroupPosts = ps.GetLatestPostsForGroup(id);

			return PartialView("~/Views/Home/NewsfeedGroups.cshtml", groupVM);
		}

		[HttpPost]
		public ActionResult ClickLike(FormCollection collection)
		{
			string postIdString = collection.Get("hidden-postId");
			int postId = Convert.ToInt32(postIdString);
			ps.AddLike(postId);

			// We know this is not "best practice", but we did not manage do sent the groupId as parameter.
			string groupIdString = collection.Get("hidden-groupId");
			int groupId = Convert.ToInt32(groupIdString);
			GroupViewModel groupVM = new GroupViewModel();
			groupVM.GroupWithId = gs.GetGroupById(groupId);
			groupVM.GroupPosts = ps.GetLatestPostsForGroup(groupId);

			return PartialView("~/Views/Home/NewsfeedGroups.cshtml", groupVM);
		}

		[HttpPost]
		public ActionResult AddComment(FormCollection collection)
		{
			string postIdString = collection.Get("hidden-postId");
			int postId = Convert.ToInt32(postIdString);

			string groupIdString = collection.Get("hidden-groupId");
			int groupId = Convert.ToInt32(groupIdString);

			string commentBody = collection.Get("comment-input");

			ps.AddNewComment(User.Identity.Name, postId, commentBody);

			GroupViewModel groupVM = new GroupViewModel();
			groupVM.GroupWithId = gs.GetGroupById(groupId);
			groupVM.GroupPosts = ps.GetLatestPostsForGroup(groupId);

			return PartialView("~/Views/Home/NewsfeedGroups.cshtml", groupVM);
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