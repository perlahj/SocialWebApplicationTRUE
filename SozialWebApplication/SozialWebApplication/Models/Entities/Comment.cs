using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SozialWebApplication.Models
{
	public class Comment
	{
		public int Id { get; set; }
		public string UserName { get; set; }
		public int PostId { get; set; }
		public DateTime DateCreated { get; set; }
		public string Body { get; set; }
	}
}