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

		private static GroupService instance;

		public static GroupService Instance
		{
			get
			{
				if (instance == null)
					instance = new GroupService();
				return instance;
			}
		}

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
		
		// Return true if group was made false otherwise.
		public bool AddNewGroup (string groupName)
		{
			// Check if another group with same name exists. 
			if ((GetGroupIdbyName(groupName) != 0) || String.IsNullOrEmpty(groupName))
			{
				return false;
			}
			
			Group g = new Group();
			g.GroupName = groupName;
			db.Groups.Add(g);
			db.SaveChanges();

			return true;
			
		}

		public void AddNewsFeed()
		{
			Group g = new Group();
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

		public bool IsUserInGroup(int groupId, string userName)
		{
			var userInGroup = (from uig in db.GroupConnections
								   where uig.GroupId == groupId &&
								   uig.UserName == userName
								   select uig).FirstOrDefault();
			
			return userInGroup != null;
		}

	}
}