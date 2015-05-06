using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SozialWebApplication.Services;
using SozialWebApplication.Models;
// Stefana
// Perla - Test
// Asta 
// Karlotta
// Audur
namespace SozialWebApplication.Controllers
{
	public class HomeController : Controller
	{
		
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			//var userService = new UserService();
			//userService.ChangeFullName(User.Identity.Name, "Jane Johnson");
			//userService.ChangeLineOfStudy(User.Identity.Name, "Computer Science");
			var groupService = new GroupService();
			groupService.AddNewGroup("SomeGroup");
			//groupService.ChangeGroupName(1, "New Group Name");
			//groupService.AddUserToGroup(1, User.Identity.Name);


			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}