﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
// Stefana
// Perla - Test
// Asta 
// Karlotta
// Audur
namespace SozialWebApplication.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Banner()
		{
			return View();
		}

        public ActionResult NameCard()
        {
            return View();
        }

        public ActionResult OthersNameCard()
        {
            return View();
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
	}
}