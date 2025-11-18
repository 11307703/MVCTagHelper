using MVCTagHelper.Models;

namespace MVCTagHelper.ViewModels
{
    public class SearchResultsViewModel
    {
        public IEnumerable<Medewerker>? Medewerkers { get; set; }
        public IEnumerable<AfdelingInfoViewModel>? AfdelingInfo { get; set; }

        public IEnumerable<Afdeling>? Afdelingen { get; set; }
        public IEnumerable<AfdelingLandViewModel>? AfdelingLandInfo { get; set; }

    }
}
