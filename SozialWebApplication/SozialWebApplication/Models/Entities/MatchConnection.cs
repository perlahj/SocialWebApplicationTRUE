using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SozialWebApplication.Models.Entities
{
	public class MatchConnection
	{
		public int Id { get; set; }
		public string UserMatching { get; set; }
		public string UserMatched { get; set; }
	}
}