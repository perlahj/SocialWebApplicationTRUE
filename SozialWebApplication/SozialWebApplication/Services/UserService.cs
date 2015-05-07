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

		public void ChangeFullName(string userName, string newFullName)
		{
			var userWithUserName = (from user in db.Users
						where user.UserName == userName
						select user).FirstOrDefault();
			userWithUserName.FullName = newFullName;
			db.SaveChanges();						 
		}
		
		public void ChangeLineOfStudy(string userName, string newLineOfStudy)
		{
			var userWithUserName = (from user in db.Users
									where user.UserName == userName
									select user).FirstOrDefault();
			userWithUserName.LineOfStudy = newLineOfStudy;
			db.SaveChanges();
		}
	}
}
