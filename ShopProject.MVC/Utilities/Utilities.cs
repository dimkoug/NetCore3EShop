using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace ShopProject.MVC.Utilities
{
    public static class Utilities
    {
        public static string IsActive(this IHtmlHelper html,
                                  string control,
                                  string action, string Path)
        {
            string url_link = "/" + control + "/" + action;
            if (action.ToLower() == "index")
            {
                url_link = "/" + control;
            }

            if (url_link == Path)
            {
                return "active";
            }
            return "";
        }


    }
}
