using System.Web;
using System.Web.Optimization;
using Dist23MVC.Models;
// ReSharper disable PossibleNullReferenceException

namespace Dist23MVC
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            RegisterStyleBundles(bundles);
            RegisterScriptBundles(bundles);

            bundles.UseCdn = true;
            //BundleTable.EnableOptimizations = false;
        }


        private static void RegisterStyleBundles(BundleCollection bundles)
        {
            //string distKey = HttpContext.Current.Session["currDist"].ToString();
            bundles.Add(new StyleBundle("~/Content/css")
                .Include("~/Content/bootstrap.min.css")
                .Include(GlobalVariables.StyleSheet)
                );

            bundles.Add(new StyleBundle("~/Content/kendo/css")
                .Include("~/Content/kendo/kendo.common-bootstrap.min.css")
                .Include("~/Content/kendo/kendo.bootstrap.min.css")
                .Include("~/Content/kendo/kendo.dataviz.min.css")
                .Include("~/Content/kendo/kendo.dataviz.bootstrap.min.css"));
        }

        private static void RegisterScriptBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Scripts/jquery", @"//ajax.aspnetcdn.com/ajax/jQuery/jquery-1.11.2.min.js")
                .Include("~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/Scripts/UIscripts")
                .Include("~/Scripts/bootstrap.min.js")
                .Include("~/Scripts/jquery-ui-1.10.4.custom.min.js"));

            bundles.Add(new ScriptBundle("~/Scripts/kendo/js")
                .Include("~/Scripts/kendo/kendo.all.min.js")
                .Include("~/Scripts/kendo/kendo.aspnetmvc.min.js"));

            bundles.Add(new ScriptBundle("~/Scripts/forms")
                .Include("~/Scripts/jquery.placeholder.js")
                .Include("~/Scripts/jquery.validate.min.js")
                .Include("~/Scripts/additional-methods.js"));

            bundles.Add(new ScriptBundle("~/Scripts/site")
                .Include("~/Scripts/main.js"));
        }
    }
}

