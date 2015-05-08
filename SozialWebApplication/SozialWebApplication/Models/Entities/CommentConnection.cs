using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SozialWebApplication.Models.Entities
{
	public class CommentConnection
	{
		public int Id { get; set; }
		public int PostId { get; set; }
		public int CommentId { get; set; }
	}
}