namespace Sporacid.Simplets.Webapp.App
{
    using System.Web.Optimization;

    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/scripts")
                .IncludeDirectory("~/Content/bootstrap/js", "*.js", true)
                .IncludeDirectory("~/Content/js/lib", "*.js", true)
                .IncludeDirectory("~/Content/js/core", "*.js", true));

            bundles.Add(new StyleBundle("~/bundles/styles")
                .Include(
                    "~/Content/bootstrap/css/bootstrap.css",
                    "~/Content/bootstrap/css/bootstrap-switch.css",
                    "~/Content/bootstrap/css/bootstrap-dark-theme.css",
                    "~/Content/font-awesome/css/font-awesome.css",
                    "~/Content/css/icons.css",
                    "~/Content/css/custom.css"));
        }
    }
}