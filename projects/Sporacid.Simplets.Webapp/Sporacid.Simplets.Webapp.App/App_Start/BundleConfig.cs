namespace Sporacid.Simplets.Webapp.App
{
    using System.Web.Optimization;

    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/scripts").Include(
                "~/Content/bootstrap/js/bootstrap.js",
                "~/Content/js/jquery-1.11.2.js",
                "~/Content/js/core/simplets-rest-client-0.9.js",
                "~/Content/js/core/simplets-core-0.9.js"));

            bundles.Add(new StyleBundle("~/bundles/styles").Include(
                "~/Content/bootstrap/css/bootstrap.css",
                "~/Content/font-awesome/css/font-awesome.css",
                "~/Content/css/local.css",
                "~/Content/css/icons.css",
                "~/Content/css/custom.css"));
        }
    }
}