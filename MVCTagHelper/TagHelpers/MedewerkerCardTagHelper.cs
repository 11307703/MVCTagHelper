using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using MVCTagHelper.ViewModels;

namespace MVCTagHelper.TagHelpers
{
    // You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
    [HtmlTargetElement("medewerker-card", Attributes = "data")]
    public class MedewerkerCardTagHelper : TagHelper
    {
        public MedewerkerCardViewModel Data { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Content.SetHtmlContent(DivCard());
        }

        private TagBuilder DivCard()
        {
            var div = new TagBuilder("div");
            div.Attributes["class"] = "card";
            div.InnerHtml.AppendHtml(DivCardTitle());
            div.InnerHtml.AppendHtml(DivCardSubtitle());
            string afdeling = $"Afdeling: {Data.AfdelingNaam}";
            div.InnerHtml.AppendHtml(DivCardText(afdeling));
            string pNr = $"Personeelsnummer: {Data.MedewerkerID}";
            div.InnerHtml.AppendHtml(DivCardText(pNr));
            return div;
        }
        private TagBuilder DivCardTitle()
        {
            var div = new TagBuilder("h1");
            div.Attributes["class"] = "card-title";
            div.InnerHtml.Append("Medewerkersfiche");
            return div;
        }
        private TagBuilder DivCardSubtitle()
        {
            var div = new TagBuilder("h2");
            div.Attributes["class"] = "card-subtitle";
            div.InnerHtml.Append("Naam: " + Data.MedewerkerNaam);
            return div;
        }
        private TagBuilder DivCardText(string content)
        {
            var div = new TagBuilder("h4");
            div.Attributes["class"] = "card-text";
            div.InnerHtml.Append(content);
            return div;
        }

//        <div class="card">
//• <h1 class="card-title">Card title</h1>
//• <h2 class="card-subtitle">Card subtitle</h2>
//• <p>
//• <h4 class="card-text">Card text l1</h4>
//• <h4 class="card-text">Card text l2</h4>
//• </p>
//• </div>


    }
}
