using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace SozialWebApplication.Models
{
	public class Post
	{
		public int Id { get; set; }
		public string UserName { get; set; }
		public int GroupId { get; set; }
		public DateTime DateCreated { get; set; }
		public string Body { get; set; }
		public int Like { get; set; }
		public PostType PostType { get; set; }
	}

	public enum PostType
	{
		Text = 1, Photo, Video
	}
}