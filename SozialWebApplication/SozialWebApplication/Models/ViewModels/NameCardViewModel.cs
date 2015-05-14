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
		// OwnNameCard.
		public ApplicationUser userWithId { get; set; }

		// Search.
		public List<ApplicationUser> AllUsers { get; set; }
		public List<ApplicationUser> AllFollowing { get; set; }
		public List<ApplicationUser> SearchResultsUsers { get; set; }

		// SearchGroups.
		public List<Group> AllUserGroups { get; set; }
		public List<Group> AllGroups { get; set; }
		public List<Group> SearchResultsGroups { get; set; }
		public string CreateGroupMessage { get; set; }

		// Matches.
        public List<ApplicationUser> AllMatches { get; set; }
	}
}