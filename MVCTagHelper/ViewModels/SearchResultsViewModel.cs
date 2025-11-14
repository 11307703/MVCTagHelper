using MVCTagHelper.Models;

namespace MVCTagHelper.ViewModels
{
    public class SearchResultsViewModel
    {
        public IEnumerable<Medewerker>? Medewerkers { get; set; }
        public IEnumerable<AfdelingInfoViewModel>? AfdelingInfo { get; set; }

    }
}
