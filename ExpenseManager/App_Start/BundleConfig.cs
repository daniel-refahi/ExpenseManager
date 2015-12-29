using System.Web;
using System.Web.Optimization;

namespace ExpenseManager
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/libs/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/libs/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/highChart").Include(
                        "~/Scripts/libs/highcharts/4.2.0/highcharts.js",
                        "~/Scripts/libs/highcharts/4.2.0/highcharts-3d.js",
                        "~/Scripts/libs/highcharts/4.2.0/modules/exporting.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/remoteCalls").Include(
                        "~/Scripts/Infrasctructure/AppURLs.js",
                        "~/Scripts/Infrasctructure/RemoteCalls.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/libs/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/libs/bootstrap.js",
                      "~/Scripts/libs/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
        }
    }
}
