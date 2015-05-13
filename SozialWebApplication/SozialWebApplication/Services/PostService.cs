using SozialWebApplication.Models;
using SozialWebApplication.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SozialWebApplication.Services
{
    public class PostService
    {
        private ApplicationDbContext db = new ApplicationDbContext();

		private static PostService instance;

		public static PostService Instance
		{
			get
			{
				if (instance == null)
					instance = new PostService();
				return instance;
			}
		}

		public void AddNewPost(string userName, int groupId, string body)
		{
			Post p = new Post();
			p.UserName = userName;
			p.GroupId = groupId;
			p.DateCreated = DateTime.Now;
			p.Body = body;
			p.Like = 0;
			db.Posts.Add(p);
			db.SaveChanges();
		}

		public List<Post> GetLatestPostsForGroup(int groupId)
		{
			var posts = (from p in db.Posts
						 where p.GroupId == groupId
						 orderby p.DateCreated descending
						 select p).Take(25).ToList();
			return posts;
		}

		public List<Post> GetAllPostsForGroup(int groupId)
		{
			var posts = (from p in db.Posts
						 where p.GroupId == groupId
						 orderby p.DateCreated descending
						 select p).ToList();
			return posts;
		}

		// Has not been tested
		public List<Post> GetLatestPostsForNewsFeed(string userName)
		{
			GroupService gs = new GroupService();
			var newsFeedId = gs.GetGroupIdbyName("News Feed");
			var allPosts = GetAllPostsForGroup(newsFeedId);
			UserService us = new UserService();
			var allFollowing = us.GetAllFollowing(userName);

			List<Post> allNewsFeedPosts = new List<Post>();
			foreach(var following in allFollowing)
			{
				var userPosts = (from up in allPosts
								 where up.UserName == following.UserName
								 select up).ToList();
				allNewsFeedPosts.AddRange(userPosts);
			}

			List<Post> newsFeedPosts = (from nfp in allNewsFeedPosts
										orderby nfp.DateCreated descending
										select nfp).Take(25).ToList();

			return newsFeedPosts;
		}


		public void AddLike(int postId)
		{
			var post = (from p in db.Posts
							  where p.Id == postId
							  select p).FirstOrDefault();

			post.Like++;
			db.SaveChanges();
		}

		// Has not been tested 
		public void RemoveLike(int postId)
		{
			var post = (from p in db.Posts
						where p.Id == postId
						select p).FirstOrDefault();

			if (post.Like > 0)
			{
				post.Like--;
			}
			
			db.SaveChanges();
		}

		public void AddNewComment(string userName, int postId,string body)
		{
			Comment c = new Comment();
			c.UserName = userName;
			c.PostId = postId;
			c.DateCreated = DateTime.Now;
			c.Body = body;
			db.Comments.Add(c);
			db.SaveChanges();
		}

		public List<Comment> GetAllCommentsForPost(int postId)
		{
			var comments = (from c in db.Comments
							where c.PostId == postId
							orderby c.DateCreated descending
							select c).Take(25).ToList();
			return comments;
		}
    }
}