using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SozialWebApplication.Models;

namespace SozialWebApplication.Services
{
	public class UserService
	{
		private ApplicationDbContext db = new ApplicationDbContext();

		public void UpdateFullName(string userName, string newFullName)
		{
			var userWithUserName = (from user in db.Users
						where user.UserName == userName
						select user).FirstOrDefault();
			userWithUserName.FullName = newFullName;
			db.SaveChanges();						 
		}
	}
}
