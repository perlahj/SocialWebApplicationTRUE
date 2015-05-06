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
			/*var userService = new UserService();
			var newFullName = "Perla Osk";
			var userName = User.Identity.Name;
			userService.ChangeFullName(userName, newFullName); */
			var userService = new UserService();
			userService.ChangeLineOfStudy(User.Identity.Name, "Computer Science");
			 
			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}