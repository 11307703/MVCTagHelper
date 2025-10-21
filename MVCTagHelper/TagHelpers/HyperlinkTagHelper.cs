using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace MVCTagHelper.TagHelpers
{
    // You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
    [HtmlTargetElement("a",Attributes="background-color")]
    public class HyperlinkTagHelper : TagHelper
    {
        public string BackgroundColor { get; set; } 
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (string.IsNullOrEmpty(BackgroundColor))
            {
                BackgroundColor = "danger";
            }
            output.Attributes.SetAttribute("class",$"btn btn-{BackgroundColor}");
        }
    }
}
