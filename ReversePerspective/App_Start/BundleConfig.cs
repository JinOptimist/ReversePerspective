using System.Web.Optimization;

namespace ReversePerspective
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                      "~/Scripts/angular.js"));

            bundles.Add(new ScriptBundle("~/bundles/app").IncludeDirectory(
                      "~/Scripts/app", "*.js", true));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                      "~/Scripts/jquery-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/css.css"));

            bundles.Add(new StyleBundle("~/Content/cutaway").Include(
                      "~/Content/cutaway.css"));
        }
    }
}