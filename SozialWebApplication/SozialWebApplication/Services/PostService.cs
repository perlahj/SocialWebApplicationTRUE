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

		// TO DO: Get latest posts for group0, only show posts from followingUsers

		// Has not been tested
		public List<Post> GetLatestPostsForGroup(int groupId)
		{
			var posts = (from p in db.Posts
						 where p.GroupId == groupId
						 select p).Take(25).ToList();
			return posts;
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
							select c).ToList();
			return comments;
		}
    }
}