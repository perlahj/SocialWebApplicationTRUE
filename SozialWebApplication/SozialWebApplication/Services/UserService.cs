﻿using System;
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

		public void ChangeProfilePicture(string userName, string newPicture)
		{
			var userWithUserName = (from user in db.Users
									where user.UserName == userName
									select user).FirstOrDefault();

			userWithUserName.ProfilePicture = newPicture;
			db.SaveChanges();
		}

		public ApplicationUser GetUserByUserName(string userName)
		{
			var user = (from u in db.Users
							where u.UserName == userName
							select u).FirstOrDefault();
			return user;
		}
		
		public ApplicationUser GetUserById(string id)
		{
			var user = (from u in db.Users
						where u.Id == id
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

		public List<ApplicationUser> SearchAllUsers(string searchStr)
		{	
			if (String.IsNullOrEmpty(searchStr))
			{
				return new List<ApplicationUser>();
			}
			
			var allUsers = GetAllUsers();
			var searchResults = new List<ApplicationUser>();
			foreach (var item in allUsers)
			{
				if (item.FullName == searchStr || item.UserName == searchStr)
				{
					searchResults.Add(item);
				}
			}
			
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

		public bool IsFollowing(string userFollowing, string userToFollow)
		{
			var followerConnection = (from dc in db.FollowerConnections
									  where dc.UserFollowing == userFollowing &&
									  dc.UserToFollow == userToFollow
									  select dc).FirstOrDefault();
			return followerConnection != null;
		}

		public List<ApplicationUser> GetAllFollowing(string userName)
		{
			List<string> userNames = (from un in db.FollowerConnections
									  where un.UserFollowing == userName
									  select un.UserToFollow).ToList();

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
									   select dmu).FirstOrDefault();
				doubleMatchesUsers.Add(doubleMatchUser);
			}

			return doubleMatchesUsers;
		}

		public void SetDefaultProfilePicture(string userName)
		{
			string pic1 = "www.iconarchive.com/download/i59028/hopstarter/superhero-avatar/Avengers-Thor.ico";
			string pic2 = "www.iconarchive.com/download/i59020/hopstarter/superhero-avatar/Avengers-Black-Widow.ico";
			string pic3 = "icons.iconarchive.com/icons/hopstarter/superhero-avatar/256/Avengers-Iron-Man-icon.png";
			string pic4 = "icons.iconarchive.com/icons/hopstarter/superhero-avatar/256/Avengers-Hulk-icon.png";
			string pic5 = "www.iconarchive.com/download/i59021/hopstarter/superhero-avatar/Avengers-Captain-America.ico";
			Random rnd = new Random();																					  
			int random = rnd.Next(1,6);
			switch (random)
			{
				case 1:
					ChangeProfilePicture(userName, "http://" + pic1);
					break;
				case 2:
					ChangeProfilePicture(userName, "http://" + pic2);
					break;
				case 3:
					ChangeProfilePicture(userName, "http://" + pic3);
					break;
				case 4:
					ChangeProfilePicture(userName, "http://" + pic4);
					break;
				default:
					ChangeProfilePicture(userName, "http://" + pic5);
					break;

			}
		}

	}
}
