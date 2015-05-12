using SozialWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SozialWebApplication.Models.Entities;

namespace SozialWebApplication.ViewModel
{
    public class GroupViewModel
    {
        public Group Group { get; set; }
        public List<Post> GroupPosts { get; set; }
		//public ApplicationUser UserWithId { get; set; }
        //public List<ApplicationUser> GroupUsers { get; set; }
    }
}