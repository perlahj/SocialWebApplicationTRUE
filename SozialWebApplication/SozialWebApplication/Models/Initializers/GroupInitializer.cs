using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SozialWebApplication.Models.Entities;
using SozialWebApplication.Services;

namespace SozialWebApplication.Models.Initializers
{
	public class GroupInitializer
	{
		private ApplicationDbContext db = new ApplicationDbContext();
		private GroupService gs = new GroupService();

		public void InitializeGroups()
		{
			Group group1 = new Group { GroupName = "Group 1" };
			Group group2 = new Group { GroupName = "Group 2" };
			Group group3 = new Group { GroupName = "Group 3" };
			db.Groups.Add(group1);
			db.Groups.Add(group2);
			db.Groups.Add(group3);
			db.SaveChanges();	
		}


	}
}