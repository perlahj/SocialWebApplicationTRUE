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
		private static UserService instance;

		public static UserService Instance
		{
			get
			{
				if (instance == null)
					instance = new UserService();
				return instance;
			}
		}
		
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
		public ApplicationUser GetUserByUserName(string userName)
		{
			var user = (from u in db.Users
							where u.UserName == userName
							select u).FirstOrDefault();
			return user;
		}


		public void AddNewFollow(string userFollowing, string userToFollow)
		{
			FollowerConnection fc = new FollowerConnection();
			fc.UserFollowing = userFollowing;
			fc.UserToFollow = userToFollow;
			db.FollowerConnections.Add(fc);
			db.SaveChanges();
		}
	 
		public void RemoveFollow(string userFollowing, string userToFollow)
		{
			var followerConnection = (from dc in db.FollowerConnections
									  where dc.UserFollowing == userFollowing &&
									  dc.UserToFollow == userToFollow
									  select dc).FirstOrDefault();
			db.FollowerConnections.Remove(followerConnection);
			db.SaveChanges();
		}

		// Has not been tested
		public List<ApplicationUser> GetAllFollowing(string userName)
		{
			List<string> userNames = (from un in db.FollowerConnections
									  where un.UserFollowing == userName
									  select un.UserFollowing).ToList();

			List<ApplicationUser> userFollowingList = new List<ApplicationUser>();
			foreach (var item in userNames)
			{
				var userToAddToList = (from u in db.Users
									   where u.UserName == item
									   select u).FirstOrDefault();
				userFollowingList.Add(userToAddToList);
			}

			return userFollowingList;
		}
		
		public void AddMatch(string userMatching, string userMatched)
		{
			MatchConnection mc = new MatchConnection();
			mc.UserMatching = userMatching;
			mc.UserMatched = userMatched;
			db.MatchConnections.Add(mc);
			db.SaveChanges();
		}

		public void RemoveMatch(string userRemoving, string userToRemove)
		{
			var matchConnection = (from mc in db.MatchConnections
								   where mc.UserMatching == userRemoving &&
								   mc.UserMatched == userToRemove
								   select mc).FirstOrDefault();
			db.MatchConnections.Remove(matchConnection);
			db.SaveChanges();

		}

		public bool IsSingleMatch(string userMatching, string userMatched)
		{
			var matchConnection = (from mc in db.MatchConnections
								   where mc.UserMatching == userMatching &&
								   mc.UserMatched == userMatched
								   select mc).FirstOrDefault();
			return matchConnection != null;
		}

		public bool IsDoubleMatch(string userMatching, string userMatched)
		{
			if (!IsSingleMatch(userMatching, userMatched))
			{
				return false;
			}

			var matchConnection = (from mc in db.MatchConnections
								   where mc.UserMatching == userMatched &&
								   mc.UserMatched == userMatching
								   select mc).FirstOrDefault();
			
			return matchConnection != null;
		}

		// Has not been tested
		public List<ApplicationUser> GetAllDoubleMatches(string userName)
		{
			// Make a list of single matches
			var singleMatches = (from sm in db.MatchConnections
								 where sm.UserMatching == userName
								 select sm).ToList();
			
			// Make a list of double matches
			List<MatchConnection> doubleMatches = new List<MatchConnection>();
			foreach(var item in singleMatches)
			{
				if (IsSingleMatch(item.UserMatched, userName))
				{
					doubleMatches.Add(item);
				}
			}

			// Make a list of users
			List<ApplicationUser> doubleMatchesUsers = new List<ApplicationUser>();

			return doubleMatchesUsers;
		}

		// TEST
			/*UserService us = new UserService();
			us.AddNewFollow(User.Identity.Name, "user1");
			us.RemoveFollow(User.Identity.Name, "user1");
			us.AddMatch(User.Identity.Name, "user1");
			us.RemoveMatch(User.Identity.Name, "user1");
			if (us.IsSingleMatch(User.Identity.Name, "user1"))
			{
				us.AddMatch("user1", User.Identity.Name);
			}
			if (us.IsDoubleMatch(User.Identity.Name, "user100"))
			{
				us.AddMatch("user99", "user98");
			}
			 */
	}
}
