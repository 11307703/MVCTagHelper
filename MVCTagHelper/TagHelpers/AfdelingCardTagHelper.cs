using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using MVCTagHelper.ViewModels;

namespace MVCTagHelper.TagHelpers
{
    // You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
    [HtmlTargetElement(Attributes = "afdeling-card")]
    public class AfdelingCardTagHelper : TagHelper
    {
        public AfdelingCard AfdelingCard { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            string content = $@"<div class='card'>";
            content += $@"<h4 class='card-title'>
            {AfdelingCard.AfdelingNaam}</h4>";

            content += $@"<p class='card-position'>
            {AfdelingCard.LandCode} - {AfdelingCard.Locatie}<p>";

            content += $@"<p class='card-description'> De afdeling
            {AfdelingCard.AfdelingNaam} ligt in {AfdelingCard.Land}.</p>";

            output.TagName = "div";
            output.Content.SetHtmlContent(content);
        }

        private void GetFormattedDiv(string content)
        {

        }
    }
}
