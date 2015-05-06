using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SozialWebApplication.Models.Entities
{
	public class FollowerConnection
	{
		public int Id { get; set; }
		public string UserFollowing { get; set; }
		public string UserToFollow { get; set; }
	}
}