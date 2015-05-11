using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SozialWebApplication.ViewModel;

namespace SozialWebApplication.Controllers
{
	public class HomeController : Controller
	{
        private GroupViewModel GroupVM = new GroupViewModel();

		public ActionResult Banner()
		{
			return View();
		}

        public ActionResult NewsfeedGroups()
        {
            return PartialView("~/Views/Home/NewsfeedGroups.cshtml", GroupVM);
        }

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}

        public ActionResult SearchGroups()
        {
            return View();
        }

        public ActionResult CheckMatch()
        {
            return View();
        }
	}
}