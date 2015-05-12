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

		// Has not been tested
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

		// Has not been tested
		public List<Post> GetLatestPostsForGroup(int groupId)
		{
			var posts = (from p in db.Posts
						 where p.GroupId == groupId
						 orderby p.DateCreated descending
						 select p).Take(25).ToList();
			return posts;
		}

		// Has not been tested
		// TO DO: select number of posts and how to choose
		// TO DO: order by dateCreated
		public List<Post> GetLatestPostsForNewsFeed(string userName)
		{
			UserService us = new UserService();
			// Takes 25 latest posts posted to news feed
			var allPosts = GetLatestPostsForGroup(0);
			var allFollowing = us.GetAllFollowing(userName);

			List<Post> newsFeedPosts = new List<Post>();
			foreach(var following in allFollowing)
			{
				var userPosts = (from up in allPosts
								 where up.UserName == following.UserName
								 select up).ToList();
				newsFeedPosts.AddRange(userPosts);
			}
			return newsFeedPosts;
		}

		// Has not been tested
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

		// Has not been tested
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

		// Has not been tested
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