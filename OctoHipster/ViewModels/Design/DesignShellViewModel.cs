using System.Collections.ObjectModel;

namespace OctoHipster.ViewModels.Design
{
    public class DesignShellViewModel : IShellViewModel
    {
        public DesignShellViewModel()
        {
            SearchText = "shiftkey";
        }

        public bool IsLoading { get; private set; }

        public string SearchText { get; set; }

        public ObservableCollection<CustomerViewModel> MatchingCustomers { get; private set; }
    }
}
