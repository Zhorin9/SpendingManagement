using System.Web;
using System.Web.Optimization;

namespace SpendingManagement
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/account").Include(
                "~/Scripts/App/controllers/accountController.js"));

            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                        "~/Scripts/App/services/recordService.js",
                        "~/Scripts/App/controllers/recordController.js"));
            
            bundles.Add(new ScriptBundle("~/bundles/form").Include(
                "~/Scripts/App/form/calculator.js",
                "~/Scripts/App/form/selectCategoriesForm.js",
                "~/Scripts/App/form/validateForm.js"));
            
            bundles.Add(new ScriptBundle("~/bundles/highcharts")
                    .Include("~/Scripts/Highcharts-4.0.1/js/highcharts.js",
                    "~/Scripts/App/charts/drawCharts.js",
                     "~/Scripts/App/charts/getDataForChart.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.min.js",
                      "~/Scripts/bootbox.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
        }
    }
}
