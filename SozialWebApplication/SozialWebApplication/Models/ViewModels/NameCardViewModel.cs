using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SozialWebApplication.Models;
using SozialWebApplication.Models.Entities;

namespace SozialWebApplication.Models.ViewModels
{
	public class NameCardViewModel
	{
		public ApplicationUser userWithId { get; set; }
		public List<ApplicationUser> AllUsers { get; set; }
		public List<ApplicationUser> SearchResultsUsers { get; set; }
		public List<Group> AllUserGroups { get; set; }
		public List<Group> AllGroups { get; set; }
		public List<Group> SearchResultsGroups { get; set; }
	}
}