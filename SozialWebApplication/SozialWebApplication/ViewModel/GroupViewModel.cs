using SozialWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SozialWebApplication.ViewModel
{
    public class GroupViewModel
    {
        public string GroupName { get; set; }
        public List<Post> GroupPost { get; set; }
        // Asta: Sleppa thessu? 
        //public List<ApplicationUser> GroupUser { get; set; }
    }
}