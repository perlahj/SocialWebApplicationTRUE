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
        private UserService us = new UserService();
		NameCardViewModel nameCardVM = new NameCardViewModel();
		
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
		public ActionResult NewsfeedGroups(FormCollection collection)
		{
			// Did not manage to send the groupId as parameter though that would have been better.
			string groupIdString = collection.Get("hidden-groupId");
			int groupId = Convert.ToInt32(groupIdString);
			
			// Post text.
			string postBody = collection.Get("text-input");
			if(!String.IsNullOrEmpty(postBody))
			{
				ps.AddNewPost(User.Identity.Name, groupId, postBody, PostType.Text);
			}
			
			// Post a photo.
			string photoBody = collection.Get("photo-input");
			if(!String.IsNullOrEmpty(photoBody))
			{
				ps.AddNewPost(User.Identity.Name, groupId, photoBody, PostType.Photo);
			}

			// Post a video.
			string videoBody = collection.Get("video-input");
			if(!String.IsNullOrEmpty(videoBody))
			{
				videoBody = ps.ParseVideoString(videoBody);
				ps.AddNewPost(User.Identity.Name, groupId, videoBody, PostType.Video);
			}

			// Like post.
			string postIdString = collection.Get("hidden-postId");
			int postId = Convert.ToInt32(postIdString);
			if(!String.IsNullOrEmpty(postIdString))
			{
				ps.AddLike(postId);
			}

			// Add a comment.
			string commentBody = collection.Get("comment-input");
			if(!String.IsNullOrEmpty(commentBody))
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
			
			// The viewmodel.
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

		public ActionResult SearchGroups()
		{
			//NameCardViewModel nameCardVM = new NameCardViewModel();
			nameCardVM.AllUserGroups = gs.GetAllGroupsForUser(User.Identity.Name);
			nameCardVM.AllGroups = gs.GetAllGroups();
			nameCardVM.SearchResultsGroups = gs.SearchAllGroups("");
            return View(nameCardVM);
		}

		[HttpPost]
		public ActionResult SearchGroups(FormCollection collection)
		{
			NameCardViewModel nameCardVM = new NameCardViewModel();

			string submitButton = collection.Get("submit");
			if (submitButton == "Create group")
			{
				string groupName = collection.Get("newgroup-name");
				
				if (gs.AddNewGroup(groupName))
				{
					int groupId = gs.GetGroupIdbyName(groupName);
					// Add the new group to list of users favorite groups.
					gs.AddUserToGroup(groupId, User.Identity.Name);
					// Post to the newly created group.
					string postBody = User.Identity.Name + " created the group " + groupName + "!";
					ps.AddNewPost(User.Identity.Name, groupId, postBody, PostType.Text);
					// Display a message. 
					nameCardVM.CreateGroupMessage = "You have created a new group.";
				}
				else
				{
					if (String.IsNullOrEmpty(groupName))
					{
						nameCardVM.CreateGroupMessage = "Please select a name for your group.";
					}
					else
					{
					   nameCardVM.CreateGroupMessage = "The group name you chose is already taken.";
					}
				}
			}

			// The viewmodel.
			nameCardVM.AllUserGroups = gs.GetAllGroupsForUser(User.Identity.Name);
			nameCardVM.AllGroups = gs.GetAllGroups();
			string searchString = collection.Get("search");
			if(String.IsNullOrEmpty(searchString))
			{
				nameCardVM.SearchResultsGroups = gs.SearchAllGroups(collection.Get(""));
			}
			else
			{
				nameCardVM.SearchResultsGroups = gs.SearchAllGroups(collection.Get("search"));
			}

            return View(nameCardVM);
		}

        public ActionResult CheckMatch()
        {
			NameCardViewModel nameCardVM = new NameCardViewModel();
            nameCardVM.AllMatches = us.GetAllDoubleMatches(User.Identity.Name);
            return View( nameCardVM);
        }
	}
}