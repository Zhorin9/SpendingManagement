using System;
using System.Web.Optimization;
namespace SpendingManagement.WebUI.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-3.3.1.min.js"
                         ));
            bundles.Add(new ScriptBundle("~/bundles/jquery-validate")
                .Include("~/Scripts/jquery.validate.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap")
                .Include("~/Scripts/bootstrap.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/highcharts")
                .Include("~/Scripts/Highcharts-4.0.1/js/highcharts.js"));

            bundles.Add(new StyleBundle("~/Content/bootstrap")
                .Include("~/Content/bootstrap-solar.css"));

            bundles.Add(new StyleBundle("~/Content/error")
                .Include("~/Content/ErrorStyles.css"));

            
        }
    }
}