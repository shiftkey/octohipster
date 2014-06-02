using System.Collections.ObjectModel;
using System.Collections.Generic;
using OctoHipster.Models;
using ReactiveUI;

namespace OctoHipster.ViewModels.Design
{
    public class DesignShellViewModel : IShellViewModel
    {
        public DesignShellViewModel()
        {
            SearchText = "shiftkey";
            IsLoading = true;
            MatchingCustomers = new ReactiveList<CustomerViewModel>();
            MatchingCustomers.Add(new CustomerViewModel { Name = "Brendan Forster", Company = "GitHub" });
            MatchingCustomers.Add(new CustomerViewModel { Name = "Paul Betts", Company = "GitHub" });
            MatchingCustomers.Add(new CustomerViewModel { Name = "Matt Ellis", Company = "JetBrains" });
            MatchingCustomers.Add(new CustomerViewModel { Name = "Phil Haack", Company = "GitHub" });
        }

        public ReactiveCommand<IEnumerable<Customer>> UpdateSearchResults { get; private set; }

        public bool IsLoading { get; private set; }
        public bool ShowError { get; private set; }

        public string SearchText { get; set; }

        public ReactiveList<CustomerViewModel> MatchingCustomers { get; private set; }
    }
}
