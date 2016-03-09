using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace KanbanBoard
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Board", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Move",
                url: "{controller}/{action}/{move}/{id}",
                defaults: new { controller = "List", action = "Lists", move = UrlParameter.Optional, id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "MemberItem",
                url: "{controller}/{action}/MemberId/{memberId}/ItemId/{itemId}",
                defaults: new { controller = "Item", action = "RemoveMember", memberId = UrlParameter.Optional, itemId = UrlParameter.Optional }
            );
        }
    }
}
