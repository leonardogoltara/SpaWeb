using System.Collections.Generic;
using System.Web.Optimization;

namespace GoltaraSolutions.SpaWeb.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            // Scripts
            RegisterScripts(bundles);
            // CSS
            RegisterStyles(bundles);

            // Minification
#if DEBUG
            BundleTable.EnableOptimizations = false;
#else
            BundleTable.EnableOptimizations = true;
#endif

        }

        public static void RegisterScripts(BundleCollection bundles)
        {
            // --- JavaScript
            bundles.Add(new ScriptBundle("~/bundles/jquery-js").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/jquery-ui.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquerymask-js").Include(
                "~/Scripts/jquery.mask.min.js",
                "~/Scripts/jquery.mask.load.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval-js")
                .Include("~/Scripts/jquery.unobtrusive-ajax.js")
                .Include("~/Scripts/jquery.validate-vsdoc.js")
                .Include("~/Scripts/jquery.validate.js")
                .Include("~/Scripts/jquery.validate.unobstrusive.js")
                .Include("~/Scripts/globalize/globalize.js")
                .Include("~/Scripts/jquery.validate.globalize.js")
                );

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr-js")
                .Include("~/Scripts/modernizr-*")
                );

            bundles.Add(new ScriptBundle("~/bundles/moment-js")
                .Include("~/Scripts/moment/moment.min.js")
                .Include("~/Scripts/moment/moment-pt-br.js")
                );

            bundles.Add(new ScriptBundle("~/bundles/bootstrap-js").Include(
                "~/Scripts/bootstrap.min.js",
                "~/Scripts/respond.js"));

            //bundles.Add(new ScriptBundle("~/bundles/datatables-js").Include(
            //    "~/Scripts/datatables/jquery.dataTables.js",
            //    "~/Scripts/datatables/dataTables.bootstrap.min.js",
            //    "~/Scripts/datatables/datatable_buttons.js",
            //    "~/Scripts/datatables/datatable_print.js",
            //    "~/Scripts/datatables/dataTables.responsive.js",
            //    "~/Scripts/datatables/datatable_sortdate.js",
            //    "~/Scripts/datatables/datatable.custom.js"));

            bundles.Add(new ScriptBundle("~/bundles/datatables-js").Include(
                "~/Scripts/datatables.net/jquery.dataTables.min.js",
                "~/Scripts/datatables.net.bs/dataTables.bootstrap.min.js",
                "~/Scripts/datatables/datatable_buttons.js",
                "~/Scripts/datatables/datatable_print.js",
                "~/Scripts/datatables/dataTables.responsive.js",
                "~/Scripts/datatables/datatable_sortdate.js",
                "~/Scripts/datatables/datatable.custom.js"));

            bundles.Add(new ScriptBundle("~/bundles/datepicker-js").Include(
                "~/Scripts/bootstrap-datepicker/bootstrap-datepicker.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/datetimepicker-js").Include(
                "~/Scripts/bootstrap-datetimepicker-master/bootstrap-datetimepicker.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/colorpicker-js").Include(
                "~/Scripts/bootstrap-colorpicker/bootstrap-colorpicker.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/select2-js").Include(
                "~/Scripts/select2/select2.full.min.js",
                "~/Scripts/select2/select2.min.js",
                "~/Scripts/select2/pt-BR.js"));

            bundles.Add(new ScriptBundle("~/bundles/popover-js").Include(
                "~/Scripts/popover/popover.js"));

            bundles.Add(new ScriptBundle("~/bundles/tooltip-js").Include(
                "~/Scripts/tooltip/tooltip.js"));

            bundles.Add(new ScriptBundle("~/bundles/fullcalendar-js").Include(
                "~/Scripts/fullcalendar/fullcalendar.min.js",
                "~/Scripts/fullcalendar/locale-all.js",
                "~/Scripts/fullcalendar/fullcalendar.custom.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery-slimscroll-js").Include(
                "~/Scripts/jquery-slimscroll/jquery.slimscroll.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/fastclick-js").Include(
                "~/Scripts/fastclick/fastclick.js"));

            bundles.Add(new ScriptBundle("~/bundles/icheck-js").Include(
                "~/Scripts/icheck/icheck.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/adminlte-js").Include(
                "~/Scripts/adminlte/adminlte.min.js"));
        }

        public static void RegisterStyles(BundleCollection bundles)
        {
            // --- CSS
            //bundles.Add(new StyleBundle("~/Content/datatables-css").Include(
            //    "~/Content/datatables/dataTables.bootstrap.css",
            //    "~/Content/datatables/dataTables.responsive.css"));

            bundles.Add(new StyleBundle("~/Content/bootstrap-css").Include(
                "~/Content/bootstrap.min.css"));

            bundles.Add(new StyleBundle("~/Content/datatables-css").Include(
                "~/Content/datatables/dataTables.responsive.css",
                "~/Content/datatables.net.bs/dataTables.bootstrap.min.css"));

            bundles.Add(new StyleBundle("~/Content/datepicker-css").Include(
                "~/Content/bootstrap-datepicker/bootstrap-datepicker.min.css"));

            bundles.Add(new StyleBundle("~/Content/datetimepicker-css").Include(
                "~/Content/bootstrap-datetimepicker-master/bootstrap-datetimepicker.min.css"));

            bundles.Add(new StyleBundle("~/Content/colorpicker-css").Include(
                "~/Content/bootstrap-colorpicker/bootstrap-colorpicker.min.css"));

            bundles.Add(new StyleBundle("~/Content/select2-css").Include(
               "~/Content/select2/select2.min.css"));

            bundles.Add(new StyleBundle("~/Content/fullcalendar-css").Include(
                "~/Content/fullcalendar/fullcalendar.min.css"));

            bundles.Add(new StyleBundle("~/Content/fullcalendar-print-css").Include(
                "~/Content/fullcalendar/fullcalendar.print.min.css"));

            bundles.Add(new StyleBundle("~/Content/fontawesome-css").Include(
                "~/Content/fontawesome/font-awesome.css"));

            //bundles.Add(new StyleBundle("~/Content/simplexTheme-css").Include(
            //    "~/Content/bootstrap-simplex.css",
            //    "~/Content/Site.css"));

            bundles.Add(new StyleBundle("~/Content/login-css").Include(
                "~/Content/Login.css").Include(
                "~/Content/Site.css"));

            bundles.Add(new StyleBundle("~/Content/icheck-css").Include(
                "~/Content/icheck/square/_all.css"));

            bundles.Add(new StyleBundle("~/Content/adminLTE-css").Include(
                "~/Content/AdminLTE/AdminLTE.min.css",
                "~/Content/AdminLTE/skins/_all-skins.min.css").Include(
                "~/Content/Site.css"));
        }
    }
    public class AsIsBundleOrderer : IBundleOrderer
    {
        public IEnumerable<BundleFile> OrderFiles(BundleContext context, IEnumerable<BundleFile> files)
        {
            return files;
        }
    }
    public class BundlesFormats
    {
        public const string PRINT = @"<link href=""{0}"" rel=""stylesheet"" type=""text/css"" media=""print"" />";
    }
}
