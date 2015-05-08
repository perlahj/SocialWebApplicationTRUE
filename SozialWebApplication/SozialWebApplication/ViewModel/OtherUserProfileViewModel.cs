using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SozialWebApplication.ViewModel
{
    public class OtherUserProfileViewModel
    {
        public string FullName { get; set; }
        public string LineOfStudy { get; set; }
        public string Email { get; set; }
        // Asta: Sleppa thessu?
        //public bool IsFollowing { get; set; }
        //public bool HasMatched { get; set; }
        //public bool IsDoubleMatch { get; set; }
    }
}