using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SozialWebApplication.Models.Entities
{
	public class Like
	{
		public int Id { get; set; }
		public int PostId { get; set; }
		public string UserName { get; set; }
	}
}