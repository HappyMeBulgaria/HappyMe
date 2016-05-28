namespace HappyMe.Web
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
                "~/Content/vendor/bootstrap/bootstrap.css",
                "~/Content/custom/site.css"));

            bundles.Add(new StyleBundle("~/Content/administration-css").Include(
                "~/Content/vendor/MvcGrid/mvc-grid.css"));
        }

        private static void RegisterScriptBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/vendor/jquery/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/ckeditor").Include(
                "~/Scripts/vendor/ckeditor/ckeditor.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/vendor/jquery/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/vendor/modernizr/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/vendor/bootstrap/bootstrap.min.js",
                "~/Scripts/vendor/respond/respond.js"));

            bundles.Add(new StyleBundle("~/bundles/administration-scripts").Include(
                "~/Scripts/custom/administration/common/editorHelper.js",
                "~/Scripts/vendor/MvcGrid/mvc-grid.js"));
        }
    }
}
