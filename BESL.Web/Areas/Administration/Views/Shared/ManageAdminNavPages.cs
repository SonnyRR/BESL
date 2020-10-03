namespace BESL.Web.Areas.Administration.Views.Shared
{
    using System;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public static class ManageAdminNavPages
    {
        public static string Index => "Games";

        public static string Teams => "Teams";

        public static string Tournaments => "Tournaments";

        public static string TournamentFormats => "TournamentFormats";

        public static string Players => "Players";

        public static string IndexNavClass(ViewContext viewContext) => PageNavClass(viewContext, Index);

        public static string TeamsNavClass(ViewContext viewContext) => PageNavClass(viewContext, Teams);

        public static string TournamentsNavClass(ViewContext viewContext) => PageNavClass(viewContext, Tournaments);

        public static string TournamentFormatsNavClass(ViewContext viewContext) => PageNavClass(viewContext, TournamentFormats);

        public static string PlayersNavClass(ViewContext viewContext) => PageNavClass(viewContext, Players);

        private static string PageNavClass(ViewContext viewContext, string page)
        {
            var activePage = viewContext.ViewData["ActivePage"] as string ?? System.IO.Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
        }
    }
}