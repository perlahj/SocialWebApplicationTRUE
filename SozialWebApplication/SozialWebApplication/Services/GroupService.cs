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

		public void AddNewGroup (string groupName)
		{
			Group g = new Group();
			g.GroupName = groupName;
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

		// Eftir ad profa
		public List<Group> GetAllGroups()
		{
			var groups = (from g in db.Groups
						  select g).ToList();
			return groups;
		}

		// Eftir ad profa
		public void AddUserToGroup(int groupId, string userName)
		{
			GroupConnection gc = new GroupConnection();
			gc.GroupId =  groupId;
			gc.UserName = userName;
			db.GroupConnections.Add(gc);
			db.SaveChanges();
		}

	}
}