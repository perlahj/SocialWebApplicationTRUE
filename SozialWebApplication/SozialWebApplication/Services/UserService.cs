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

		public void ChangeEmail(string userName, string newEmail)
		{
			var userWithUserName = (from user in db.Users
									where user.UserName == userName
									select user).FirstOrDefault();
			userWithUserName.Email = newEmail;
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

		public List<ApplicationUser> GetAllUsers()
		{
			var allUsers = (from au in db.Users
							orderby au.FullName ascending
							  select au).ToList();
			
			return allUsers;
		}
		// Has not been tested
		public List<ApplicationUser> SearchAllUsers(string searchStr)
		{
			if(String.IsNullOrEmpty(searchStr))
			{
				return new List<ApplicationUser>();
			}
			var allUsers = GetAllUsers();
			var searchResults = (from r in allUsers
								 where r.FullName.StartsWith(searchStr)
								 orderby r.FullName ascending
								 select r).ToList();
			
			//List<ApplicationUser> searchResults = new List<ApplicationUser>();

						/*query = (from Schl in model.Scholars
			where Schl.ScholarName.StartsWith(txtSearch.Text)

			orderby Schl.ScholarName
			select Schl);
			 */

			//foreach (var item in allUsers)
			/*{
			   var result = allUsers.Find(x => x.FullName.Contains(searchStr));
				searchResults.Add(result);	
			}*/

				//parts.Find(x => x.PartName.Contains("seat")));

			/*var searchResults = (from sr in allUsers
								 where (m => sr.FullName.StartsWith(searchStr))
								 select sr).ToList(); */
			return searchResults;
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

		// Needs to be tested!
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

			// Make a list of users not usernames
			List<ApplicationUser> doubleMatchesUsers = new List<ApplicationUser>();
			foreach (var item in doubleMatches)
			{
				var doubleMatchUser = (from dmu in db.Users
									   where dmu.UserName == item.UserMatched
									   select dmu).SingleOrDefault();
				doubleMatchesUsers.Add(doubleMatchUser);
			}


			return doubleMatchesUsers;
		}

	}
}
