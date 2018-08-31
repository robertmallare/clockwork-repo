using System;
using System.Web.Optimization;

namespace Clockwork.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));


            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                        "~/Scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                        "~/Scripts/angular.js"
                        ,"~/Scripts/ng-table.js"
                        ,"~/Scripts/libs/controllers/HomeController.js"
                        ,"~/Scripts/libs/apps/app.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css"
                    , "~/Content/ng-table.css"
                    , "~/Content/site.css"));
        }
    }
}
