using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShopProject.MVC.Helpers
{

    [HtmlTargetElement("a", Attributes = "active-when")]
    public class ATagHelper : TagHelper
    {
        public string ActiveWhen { get; set; }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContextData { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (ActiveWhen == null)
                return;

            var targetController = ActiveWhen.Split("/")[1];
            var targetAction = ActiveWhen.Split("/")[2];

            var currentController = ViewContextData.RouteData.Values["controller"].ToString();
            var currentAction = ViewContextData.RouteData.Values["action"].ToString();

            if (currentController.Equals(targetController) && currentAction.Equals(targetAction))
            {
                if (output.Attributes.ContainsName("class"))
                {
                    output.Attributes.SetAttribute("class", $"{output.Attributes["class"].Value} active");
                }
                else
                {
                    output.Attributes.SetAttribute("class", "active");
                }
            }
        }
    }
}

