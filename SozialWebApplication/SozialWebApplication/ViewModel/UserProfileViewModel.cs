using SozialWebApplication.Models;
using SozialWebApplication.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SozialWebApplication.ViewModel
{
    public class UserProfileViewModel
    {
        public string FullName { get; set; }
        public string LineOfStudy { get; set; }
        public List<ApplicationUser> Followings { get; set; }
        public List<ApplicationUser> DoubleMatches { get; set; }
        public List<Group> GroupsJoined { get; set; }
    }
}