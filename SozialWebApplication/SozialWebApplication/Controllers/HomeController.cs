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
        private UserService us = new UserService();
		
		public ActionResult Banner()
		{
			return View();
		}

        public ActionResult NewsfeedGroups(int id)
        {
			GroupViewModel groupVM = new GroupViewModel();
			int newsFeedId = gs.GetGroupIdbyName("News Feed");
			if (id == newsFeedId)
			{
				groupVM.GroupWithId = gs.GetGroupById(id);
				groupVM.GroupPosts = ps.GetLatestPostsForNewsFeed(User.Identity.Name);
			}
			else
			{
				groupVM.GroupWithId = gs.GetGroupById(id);
				groupVM.GroupPosts = ps.GetLatestPostsForGroup(id);
			}

			return View(groupVM);
        }

		[HttpPost]
		public ActionResult NewsfeedGroups(int id, FormCollection collection)
		{
			string postBody = collection.Get("post-input");
			ps.AddNewPost(User.Identity.Name, id, postBody);

			GroupViewModel groupVM = new GroupViewModel();
			int newsFeedId = gs.GetGroupIdbyName("News Feed");
			if (id == newsFeedId)
			{
				groupVM.GroupWithId = gs.GetGroupById(id);
				groupVM.GroupPosts = ps.GetLatestPostsForNewsFeed(User.Identity.Name);
			}
			else
			{
				groupVM.GroupWithId = gs.GetGroupById(id);
				groupVM.GroupPosts = ps.GetLatestPostsForGroup(id);
			}
			

			return View(groupVM);
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

		public ActionResult SearchGroups()
		{
			nameCardVM.AllUserGroups = gs.GetAllGroupsForUser(User.Identity.Name);
			nameCardVM.AllGroups = gs.GetAllGroups();
			nameCardVM.SearchResultsGroups = gs.SearchAllGroups("");
            return View(nameCardVM);
		}

		[HttpPost]
		public ActionResult SearchGroups(FormCollection collection)
		{
			nameCardVM.AllUserGroups = gs.GetAllGroupsForUser(User.Identity.Name);
			nameCardVM.AllGroups = gs.GetAllGroups();
			nameCardVM.SearchResultsGroups = gs.SearchAllGroups(collection.Get("search"));
            return View(nameCardVM);
		}

		[HttpPost]
		public ActionResult AddNewGroup(FormCollection collection)
		{
			string groupName = collection.Get("newgroup-name");
			gs.AddNewGroup(groupName);
			int groupId = gs.GetGroupIdbyName(groupName);
			// Register user to the new group.
			gs.AddUserToGroup(groupId, User.Identity.Name);
			// Post to the newly created group.
			string postBody = User.Identity.Name + " created the group " + groupName + "!";
			
			ps.AddNewPost(User.Identity.Name, groupId, postBody);
			
			nameCardVM.AllUserGroups = gs.GetAllGroupsForUser(User.Identity.Name);
			nameCardVM.AllGroups = gs.GetAllGroups();
			nameCardVM.SearchResultsGroups = gs.SearchAllGroups(collection.Get(""));
			return View("SearchGroups", nameCardVM);
		}



        public ActionResult CheckMatch()
        {
            //us.AddMatch("Palli", "astagis");
            //us.AddMatch("Malli", "astagis");
            // senda model inn i rett view og tha er haegt ad vinna med gognin
            nameCardVM.AllMatches = us.GetAllDoubleMatches(User.Identity.Name);

            return PartialView("~/Views/Home/CheckMatch.cshtml", nameCardVM);
        }
	}
}