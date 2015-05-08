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

        //breytum ekki username ne groupid
        public void ChangeBody(string body, string newBody)
        {
            var postWithBody = (from b in db.Posts
                                where b.Body == body
                                select b).FirstOrDefault();
            postWithBody.Body = newBody;
            db.SaveChanges();

        }

        public List<Post> GetLatestPosts()
        {
            var posts = (from p in db.Posts
                         select p).ToList();
            return posts;
        }

        public void AddNewPost(string post)
        {
            Post p = new Post();
            p.Body = post;
            db.Posts.Add(p);
            db.SaveChanges();
        }
        
      /*  public void AddLike()
        {
            Like l = new Like();
            db.Likes.Add(1);
            db.SaveChanges();     
        }
	   */

        public void RemoveLike()
        {
        }

        public List<Comment> GetAllComments()
        {
            var comments = (from c in db.Comments
                            select c).ToList();
            return comments;
        }

        public void AddNewComment(string comment)
        {
            Comment c = new Comment();
            c.Body = comment;
            db.Comments.Add(c);
            db.SaveChanges();
        }

    }
}