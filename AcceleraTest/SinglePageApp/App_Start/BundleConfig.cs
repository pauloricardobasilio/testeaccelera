using System.Web;
using System.Web.Optimization;

namespace SinglePageApp
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            BundleTable.EnableOptimizations = true;

            bundles.Add(new ScriptBundle("~/bundles/knockout").Include(
                "~/Scripts/knockout-{version}.js",
                "~/Scripts/knockout.validation.js",
                "~/Scripts/knockout.localization.js",
                "~/Scripts/knockout.bindings.js",
                "~/Scripts/knockout.appconfig.js",
                "~/Scripts/format.js"));

            bundles.Add(new ScriptBundle("~/bundles/app/colaboratormodel")
                .Include("~/Scripts/app/colaborator.model.js"));

            bundles.Add(new ScriptBundle("~/bundles/app/colaboratorlistviewmodel").Include(
                "~/Scripts/app/colaboratorlist.viewmodel.js"));

            bundles.Add(new ScriptBundle("~/bundles/app/colaboratoreditviewmodel").Include(
                "~/Scripts/app/colaboratoredit.viewmodel.js"));

            bundles.Add(new ScriptBundle("~/bundles/appstart").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/sammy-{version}.js",
                "~/Scripts/app.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));
        }
    }
}