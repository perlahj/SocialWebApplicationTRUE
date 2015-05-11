using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SozialWebApplication.Models;

namespace SozialWebApplication.Models.ViewModels
{
	public class NameCardViewModel
	{
		public ApplicationUser userWithId { get; set; }
		public List<ApplicationUser> AllUsers { get; set; }
		public List<ApplicationUser> SearchResultsUsers { get; set; }
	}
}