using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using OctoHipster.Logic;
using OctoHipster.Services;
using ReactiveUI;

namespace OctoHipster.ViewModels
{
    public interface IShellViewModel
    {
        ReactiveCommand<object> UpdateSearchResults { get; }
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

            UpdateSearchResults = ReactiveCommand.Create();
            UpdateSearchResults.Subscribe(async o =>
            {
                var value = o as string;

                MatchingCustomers.Clear();

                if (String.IsNullOrWhiteSpace(value)) return;

                IsLoading = true;

                var customers = await _customerService.GetByName(value);

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

                IsLoading = false;
            });

            this.WhenAnyValue(x => x.SearchText)
                .Throttle(TimeSpan.FromMilliseconds(500), RxApp.MainThreadScheduler)
                .Subscribe(x => UpdateSearchResults.TryExecute(x));
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

        public ReactiveCommand<object> UpdateSearchResults { get; private set; }

        public ObservableCollection<CustomerViewModel> MatchingCustomers { get; private set; }
    }
}
