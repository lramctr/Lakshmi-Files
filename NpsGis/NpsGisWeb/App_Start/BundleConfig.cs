using System.Web.Optimization;

namespace Nps.Gis.Web
{
    /// <summary>
    /// Bundle Configuration for styles and scripts
    /// </summary>
    public class BundleConfig
    {
        /// <summary>
        /// Register Bundles
        /// For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        /// </summary>
        public static void RegisterBundles(BundleCollection bundles)
        {
            // Styles
            bundles.Add(new StyleBundle("~/Styles/site").Include(
                "~/Content/site.css",
                "~/Content/jquery-ui-autocomplete.css"));

            bundles.Add(new StyleBundle("~/Styles/pivot").Include(
                "~/Content/pivotviewer.css"));

            // Scripts
            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/Scripts/modernizr").Include(
                "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/Scripts/app").Include(
                "~/Scripts/app.js"));

            bundles.Add(new ScriptBundle("~/Scripts/pivot").Include(
                "~/Scripts/easing.js",
                "~/Scripts/jquery.mousewheel.min.js",
                "~/Scripts/pivotviewer.js"));
        }
    }
}
