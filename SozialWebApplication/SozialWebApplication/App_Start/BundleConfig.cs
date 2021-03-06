﻿using System.Web;
using System.Web.Optimization;

namespace SozialWebApplication
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/CSS/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/aWholeLottaJavascipt.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/CSS/bootstrap.css",
                      "~/Content/CSS/site.css", "~/Content/CSS/namecard.css", "~/Content/CSS/Banner.css", "~/Content/CSS/NewsfeedGroups.css", "~/Content/CSS/Search.css", "~/Content/CSS/Matches.css"));
        }
    }
}
