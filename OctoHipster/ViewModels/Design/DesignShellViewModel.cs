using System.Collections.ObjectModel;

namespace OctoHipster.ViewModels.Design
{
    public class DesignShellViewModel : IShellViewModel
    {
        public DesignShellViewModel()
        {
            SearchText = "shiftkey";
            IsLoading = true;
            MatchingCustomers = new ObservableCollection<CustomerViewModel>();
            MatchingCustomers.Add(new CustomerViewModel { Name = "Brendan Forster", Company = "GitHub" });
            MatchingCustomers.Add(new CustomerViewModel { Name = "Paul Betts", Company = "GitHub" });
            MatchingCustomers.Add(new CustomerViewModel { Name = "Matt Ellis", Company = "JetBrains" });
            MatchingCustomers.Add(new CustomerViewModel { Name = "Phil Haack", Company = "GitHub" });
        }

        public bool IsLoading { get; private set; }
        public bool ShowError { get; private set; }

        public string SearchText { get; set; }

        public ObservableCollection<CustomerViewModel> MatchingCustomers { get; private set; }
    }
}
