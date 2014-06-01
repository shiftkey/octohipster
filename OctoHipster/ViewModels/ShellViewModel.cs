using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using OctoHipster.Services;

namespace OctoHipster.ViewModels
{
    public interface IShellViewModel
    {
        bool IsLoading { get; }
        bool ShowError { get; }
        string SearchText { get; set; }
        ObservableCollection<CustomerViewModel> MatchingCustomers { get; }
    }

    public class ShellViewModel : ViewModelBase, IShellViewModel
    {
        readonly ICustomerService _customerService;

        public ShellViewModel(ICustomerService customerService)
        {
            _customerService = customerService;

            MatchingCustomers = new ObservableCollection<CustomerViewModel>();
        }

        bool _isLoading;
        public bool IsLoading
        {
            get { return _isLoading; }
            private set
            {
                _isLoading = value;
                RaisePropertyChanged(() => IsLoading);
            }
        }

        bool _showError;
        public bool ShowError
        {
            get { return _showError; }
            set
            {
                _showError = value;
                RaisePropertyChanged(() => ShowError);
            }
        }

        string _searchText;
        
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                // hahahaha this is just horrible
                // do not try this at home
                // use proper commanding
                UpdateSearchResults(value);
            }
        }

        // TPL nerd notes - with .NET 4.5, exceptions
        // raised by awaitable tasks are handled if you
        // the method signature is "async Task" - so they'll
        // be swallowed here and not bring down the app
        async Task UpdateSearchResults(string value)
        {
            MatchingCustomers.Clear();

            if (String.IsNullOrWhiteSpace(value)) return;

            ShowError = false;
            IsLoading = true;

            try
            {
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
