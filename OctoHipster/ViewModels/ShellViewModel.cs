using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
using OctoHipster.Services;
using ReactiveUI;

namespace OctoHipster.ViewModels
{
    public interface IShellViewModel
    {
        bool IsLoading { get; }
        bool ShowError { get; }
        string SearchText { get; set; }
        ObservableCollection<CustomerViewModel> MatchingCustomers { get; }
    }

    public class ShellViewModel : ReactiveObject, IShellViewModel
    {
        readonly ICustomerService _customerService;

        public ShellViewModel(ICustomerService customerService)
        {
            _customerService = customerService;

            MatchingCustomers = new ObservableCollection<CustomerViewModel>();

            this.WhenAnyValue(x => x.SearchText)
                .SelectMany(_ => UpdateSearchResults().ToObservable())
                .Subscribe();
        }

        bool _isLoading;
        public bool IsLoading
        {
            get { return _isLoading; }
            private set { this.RaiseAndSetIfChanged(ref _isLoading, value); }
        }

        bool _showError;
        public bool ShowError
        {
            get { return _showError; }
            private set { this.RaiseAndSetIfChanged(ref _showError, value); }
        }

        string _searchText;
        public string SearchText
        {
            get { return _searchText; }
            set { this.RaiseAndSetIfChanged(ref _searchText, value); }
        }

        // TPL nerd notes - with .NET 4.5, exceptions
        // raised by awaitable tasks are handled if you
        // the method signature is "async Task" - so they'll
        // be swallowed here and not bring down the app
        public async Task UpdateSearchResults()
        {
            MatchingCustomers.Clear();

            // depends on external state, hrm
            var searchText = SearchText;

            // looks like a precondition
            if (String.IsNullOrWhiteSpace(SearchText)) return;

            // ha ha mutable state
            ShowError = false;
            IsLoading = true;

            try
            {
                // this service is so unreliable
                var customers = await _customerService.GetByName(searchText);

                // map the results and populate the collection
                foreach (var c in customers)
                {
                    MatchingCustomers.Add(new CustomerViewModel
                    {
                        Name = c.Name,
                        Company = c.Company,
                        Contact = c.Contact,
                        DateOfBirth = c.DateOfBirth
                    });
                }
            }
            catch (Exception)
            {
                ShowError = true;
            }
            IsLoading = false;
        }

        public ObservableCollection<CustomerViewModel> MatchingCustomers { get; private set; }
    }
}
