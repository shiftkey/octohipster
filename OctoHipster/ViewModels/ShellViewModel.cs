using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Linq;
using System.Threading.Tasks;
using OctoHipster.Logic;
using OctoHipster.Models;
using OctoHipster.Services;
using ReactiveUI;

namespace OctoHipster.ViewModels
{
    public interface IShellViewModel
    {
        ReactiveCommand<IEnumerable<Customer>> UpdateSearchResults { get; }
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

            UpdateSearchResults = ReactiveCommand.CreateAsyncTask(o =>
            {
                var value = o as string;

                MatchingCustomers.Clear();
                ShowError = false;

                if (String.IsNullOrWhiteSpace(value))
                {
                    // making a decision here to not fetch the world
                    return Task.FromResult(Enumerable.Empty<Customer>());
                }

                return _customerService.GetByName(value);
            });

            UpdateSearchResults.ThrownExceptions.Subscribe(ex =>
            {
                // TODO: log exception
                ShowError = true;
            });

            UpdateSearchResults.Subscribe(customers =>
            {
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
            });

            _isLoading = UpdateSearchResults.IsExecuting
                .ToProperty(this, vm => vm.IsLoading);

            this.WhenAnyValue(x => x.SearchText)
                .Throttle(TimeSpan.FromMilliseconds(500), RxApp.MainThreadScheduler)
                .Subscribe(x => UpdateSearchResults.TryExecute(x));
        }

        readonly ObservableAsPropertyHelper<bool> _isLoading;
        public bool IsLoading
        {
            get { return _isLoading.Value; }
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

        public ReactiveCommand<IEnumerable<Customer>> UpdateSearchResults { get; private set; }

        public ObservableCollection<CustomerViewModel> MatchingCustomers { get; private set; }
    }
}
