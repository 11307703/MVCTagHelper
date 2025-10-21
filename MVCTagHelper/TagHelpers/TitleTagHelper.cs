using Microsoft.AspNetCore.Razor.TagHelpers;

namespace MVCTagHelper.TagHelpers
{
    [HtmlTargetElement("title-tag", Attributes ="title-text")]
    public class TitleTagHelper : TagHelper
    {
        public string TitleText { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Content.SetHtmlContent($"<h1 class='display-4'>{TitleText}</h1>");
        }
    }
}
