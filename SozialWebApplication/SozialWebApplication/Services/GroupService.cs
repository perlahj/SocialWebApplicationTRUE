using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SozialWebApplication.Models;

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
	}
}