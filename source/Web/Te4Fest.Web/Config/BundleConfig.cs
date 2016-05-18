namespace Te4Fest.Web
{
    using System.Web.Optimization;

    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            RegisterScriptBundles(bundles);
            RegisterStyleBundles(bundles);
        }

        private static void RegisterStyleBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/vendors/bootstrap/bootstrap.css",
                "~/Content/custom/site.css"));

            bundles.Add(new StyleBundle("~/Content/administration-css").Include(
                "~/Content/vendors/MvcGrid/mvc-grid.css"));
        }

        private static void RegisterScriptBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/vendor/jquery/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/vendor/jquery/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/vendor/modernizr/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/vendor/bootstrap/bootstrap.js",
                "~/Scripts/vendor/respond/respond.js"));

            bundles.Add(new StyleBundle("~/bundles/administration-scripts").Include(
                "~/Scripts/vendors/MvcGrid/mvc-grid.js"));
        }
    }
}
