using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SozialWebApplication.Models;
using SozialWebApplication.Models.Entities;

namespace SozialWebApplication.Services
{
	public class GroupService
	{
		private ApplicationDbContext db = new ApplicationDbContext();

		public Group GetGroupById(int id)
		{
			var groupWithId = (from gwi in db.Groups
							   where gwi.Id == id
							   select gwi).FirstOrDefault();
			return groupWithId;
		}

		// Returns the id of the most recently created group with groupName.
		public int GetGroupIdbyName(string groupName)
		{
			var groupId = (from gi in db.Groups
						   where gi.GroupName == groupName
						   orderby gi.Id descending
						   select gi.Id).FirstOrDefault();
			return groupId;
		}
		
		// It is possible to add multiple groups with same name.
		public void AddNewGroup (string groupName)
		{
			Group g = new Group();
			g.GroupName = groupName;
			db.Groups.Add(g);
			db.SaveChanges();
		}

		public void AddNewsFeed()
		{
			Group g = new Group();
			g.Id = 999;
			g.GroupName = "News Feed";
			db.Groups.Add(g);
			db.SaveChanges();
		}
		
		public void ChangeGroupName(int groupId, string newGroupName)
		{
			var groupWithId = (from g in db.Groups
									where g.Id == groupId
									select g).FirstOrDefault();
			groupWithId.GroupName = newGroupName;
			db.SaveChanges();
		}
		
		public List<Group> GetAllGroups()
		{
			var groups = (from g in db.Groups
						  select g).ToList();
			return groups;
		}

		public void AddUserToGroup(int groupId, string userName)
		{
			GroupConnection gc = new GroupConnection();
			gc.GroupId =  groupId;
			gc.UserName = userName;
			db.GroupConnections.Add(gc);
			db.SaveChanges();
		}

		public void RemoveUserFromGroup(int groupId, string userName)
		{
			var groupConnection = (from gc in db.GroupConnections
									 where gc.GroupId == groupId
									select gc).FirstOrDefault();
		
			db.GroupConnections.Remove(groupConnection);
			db.SaveChanges();
		}

		public List<Group> GetAllGroupsForUser(string userName)
		{
			List<int> groupIds = (from gc in db.GroupConnections
									where gc.UserName == userName
									select gc.GroupId).ToList();
			
			List<Group> groups = new List<Group>();
			foreach(var gId in groupIds)
			{
				var group = (from g in db.Groups
							where g.Id == gId
							select g).FirstOrDefault();
				groups.Add(group);
			}
	
			return groups;
		}

		public List<Group> SearchAllGroups(string searchStr)
		{
			if (String.IsNullOrEmpty(searchStr))
			{
				return new List<Group>();
			}
			var allGroups = GetAllGroups();

			var searchResults = new List<Group>();
			foreach (var item in allGroups)
			{
				if (item.GroupName == searchStr)
				{
					searchResults.Add(item);
				}
			}
	

			return searchResults;
		}

		// TESTS
		//var userService = new UserService();
		//userService.ChangeFullName(User.Identity.Name, "Jane Johnson");
		//userService.ChangeLineOfStudy(User.Identity.Name, "Computer Science");
		//var groupService = new GroupService();
		//groupService.AddNewGroup("SomeGroup");
		//groupService.ChangeGroupName(1, "New Group Name");
		//List<Group> groups = groupService.GetAllGroups();
		//groupService.AddUserToGroup(2, User.Identity.Name);
		//List<Group> userGroups = groupService.GetAllGroupsForUser(User.Identity.Name);
		//foreach (var g in userGroups)
		//{
		//	groupService.AddUserToGroup(g.Id, User.Identity.Name);
		//} 
		//groupService.RemoveUserFromGroup(2, User.Identity.Name);
	


	}
}