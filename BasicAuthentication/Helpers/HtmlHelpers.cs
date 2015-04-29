using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BasicAuthentication.Helpers
{
    public static class HtmlHelpers
    {
        public static MvcHtmlString IsIndex(this HtmlHelper html)
        {
            string cssClass = "demo";
            string currentAction = (string)html.ViewContext.RouteData.Values["action"];
            string currentController = (string)html.ViewContext.RouteData.Values["controller"];

            return new MvcHtmlString("Home" == currentController && "Index" == currentAction ? cssClass : String.Empty);
        }

        public static MvcHtmlString TruncateText(this HtmlHelper html, string texto, int length)
        {
            if (texto == null)
            {
                return new MvcHtmlString(String.Empty);
            }

            if (texto.Length > length)
            {
                texto = texto.Substring(0, length) + "...";
            }

            return new MvcHtmlString(texto);
        }
    }
}