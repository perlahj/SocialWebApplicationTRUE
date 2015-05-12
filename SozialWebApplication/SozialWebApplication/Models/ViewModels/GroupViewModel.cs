using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SozialWebApplication.Models.Entities;

namespace SozialWebApplication.Models.ViewModels
{
	public class GroupViewModel
	{
		public Group GroupWithId { get; set; }
		public List<Post> GroupPosts { get; set; }
		public List<Comment> CommentsForPost { get; set; }
	}
}