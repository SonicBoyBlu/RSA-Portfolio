using System.Web.Optimization;

namespace Acme
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/sytles").Include(
                 "~/Content/styles/site.min.css"
            ));


            bundles.Add(new ScriptBundle("~/bundles/plugins").Include(
                "~/Content/plugins/serializeObject.js",
                "~/Content/plugins/putCursorAtEnd.js",
                "~/Content/plugins/forms.js",
                "~/Content/plugins/bootstrap-actions.js",
                "~/Content/plugins/autocomplete/jquery.autocomplete.js"
            ));
            /*
            bundles.Add(new ScriptBundle("~/bundles/plugins").IncludeDirectory(
            "~/Content/plugins", "*.js", true
            ));
            */

            //bundles.Add(new ScriptBundle("~/bundles/plugins-npm").Include(
            //  "~/Scripts/toastr.min.js"
            //));

            
            bundles.Add(new Bundle("~/bundles/app")
                .IncludeDirectory("~/Content/scripts", "*.js", true
            ));

            bool isOptimize = true;
            #if DEBUG
                isOptimize = false;
            #endif
            BundleTable.EnableOptimizations = isOptimize;
        }
    }
}
