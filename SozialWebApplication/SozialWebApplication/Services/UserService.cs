using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SozialWebApplication.Models;
using SozialWebApplication.Models.Entities;

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

		// Has not been tested
		public void AddNewFollow(string userFollowing, string userToFollow)
		{
			FollowerConnection fc = new FollowerConnection();
			fc.UserFollowing = userFollowing;
			fc.UserToFollow = userToFollow;
			db.FollowerConnections.Add(fc);
			db.SaveChanges();
		}
	 
		// Has not been tested
		public void RemoveFollow(string userFollowing, string userToFollow)
		{
			var followerConnection = (from dc in db.FollowerConnections
									  where dc.UserFollowing == userFollowing &&
									  dc.UserToFollow == userToFollow
									  select dc).FirstOrDefault();
			db.FollowerConnections.Remove(followerConnection);
			db.SaveChanges();
		}
		
		public void AddMatch(string userMatching, string userMatched)
		{
			MatchConnection mc = new MatchConnection();
			mc.UserMatching = userMatching;
			mc.UserMatched = userMatched;
			db.MatchConnections.Add(mc);
			db.SaveChanges();
		}

		/*public void RemoveMatch(string userRemoving, string userRemoved)
		{
			var matchConnection = (from mc in db.MatchConnections
									   where mc.UserMatching == userRemoved &&
									   mc.UserMatched)
		}  */
	}
}
