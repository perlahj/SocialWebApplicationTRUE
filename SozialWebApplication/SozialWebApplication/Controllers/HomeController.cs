using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SozialWebApplication.Models.ViewModels;
using SozialWebApplication.Models;
using SozialWebApplication.Services;

namespace SozialWebApplication.Controllers
{
	public class HomeController : Controller
	{
		private GroupService gs = new GroupService();
		private PostService ps = new PostService();
		private NameCardViewModel nameCardVM = new NameCardViewModel();
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
		//public ActionResult NewsfeedGroups(int groupId, FormCollection collection)
		public ActionResult NewsfeedGroups(FormCollection collection)
		{
			// Get the group id. We know this is not best practice.
			string groupIdString = collection.Get("hidden-groupId");
			int groupId = Convert.ToInt32(groupIdString);
			
			// Post text.
			string postBody = collection.Get("text-input");
			if (!String.IsNullOrEmpty(postBody))
			{
				ps.AddNewPost(User.Identity.Name, groupId, postBody, PostType.Text);
			}
			
			// Post a photo.
			string photoBody = collection.Get("photo-input");
			if (!String.IsNullOrEmpty(photoBody))
			{
				ps.AddNewPost(User.Identity.Name, groupId, photoBody, PostType.Photo);
			}

			// Post a video.
			string videoBody = collection.Get("video-input");
			if (!String.IsNullOrEmpty(videoBody))
			{
				videoBody = ps.ParseVideoString(videoBody);
				ps.AddNewPost(User.Identity.Name, groupId, videoBody, PostType.Video);
			}

			// Like post.
			string postIdString = collection.Get("hidden-postId");
			int postId = Convert.ToInt32(postIdString);
			if (!String.IsNullOrEmpty(postIdString))
			{
				ps.AddLike(postId);
			}

			// Add a comment.
			string commentBody = collection.Get("comment-input");
			if (!String.IsNullOrEmpty(commentBody))
			{
				ps.AddNewComment(User.Identity.Name, postId, commentBody);
			}

			// Add or remove group from favorite.
			string action = collection.Get("hidden-favgroup");
			if (!String.IsNullOrEmpty(action))
			{
				if (action == "add-group")
				{
					if (!gs.IsUserInGroup(groupId, User.Identity.Name))
					{
						gs.AddUserToGroup(groupId, User.Identity.Name);
					}
				}
				else if (action == "remove-group")
				{
					if (gs.IsUserInGroup(groupId, User.Identity.Name))
					{
						gs.RemoveUserFromGroup(groupId, User.Identity.Name);
					}
				}
			}
			
			// Make the viewmodel.
			GroupViewModel groupVM = new GroupViewModel();
			int newsFeedId = gs.GetGroupIdbyName("News Feed");
			if (groupId == newsFeedId)
			{
				groupVM.GroupPosts = ps.GetLatestPostsForNewsFeed(User.Identity.Name);
			}
			else
			{
				
				groupVM.GroupPosts = ps.GetLatestPostsForGroup(groupId);
			}
			groupVM.GroupWithId = gs.GetGroupById(groupId);
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

            return View("NewsfeedGroups", groupVM);
		}

		[HttpPost]
		public ActionResult ClickAddToFavorite(FormCollection collection)
		{
			string groupIdString = collection.Get("hidden-groupId");
			int groupId = Convert.ToInt32(groupIdString);

			string action = collection.Get("hidden-favgroup");
			if (action == "add-group")
			{
				if (!gs.IsUserInGroup(groupId, User.Identity.Name))
				{
					gs.AddUserToGroup(groupId, User.Identity.Name);
				}
			}
			else if (action == "remove-group")
			{
				if (gs.IsUserInGroup(groupId, User.Identity.Name))
				{
					gs.RemoveUserFromGroup(groupId, User.Identity.Name);
				}
			}

			GroupViewModel groupVM = new GroupViewModel();
			groupVM.GroupWithId = gs.GetGroupById(groupId);
			groupVM.GroupPosts = ps.GetLatestPostsForGroup(groupId);

			return View("NewsfeedGroups", groupVM);
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

			return RedirectToAction("NewsfeedGroups", groupVM);
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
			// If user creates a group without a name.
			if(String.IsNullOrEmpty(groupName))
			{
				Random rnd = new Random();
				int random = rnd.Next(1, 999);
				string randomNumber = Convert.ToString(random);
				groupName = "Group" + randomNumber;
			}
			gs.AddNewGroup(groupName);
			int groupId = gs.GetGroupIdbyName(groupName);
			// Add the new group to list of favorite groups.
			gs.AddUserToGroup(groupId, User.Identity.Name);
			// Post to the newly created group.
			string postBody = User.Identity.Name + " created the group " + groupName + "!";
			ps.AddNewPost(User.Identity.Name, groupId, postBody, PostType.Text);
			
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

            return View( nameCardVM);
        }
	}
}