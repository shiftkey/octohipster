using System;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using OctoHipster.Services;

namespace OctoHipster.ViewModels
{
    public interface IShellViewModel
    {
        bool IsLoading { get; }
        string SearchText { get; set; }
        ObservableCollection<CustomerViewModel> MatchingCustomers { get; }
    }

    public class ShellViewModel : ViewModelBase, IShellViewModel
    {
        readonly CustomerService _customerService;

        public ShellViewModel(CustomerService customerService, OrderService orderService)
        {
            _customerService = customerService;

            MatchingCustomers = new ObservableCollection<CustomerViewModel>();
        }

        public bool IsLoading { get; private set; }

        string _searchText;
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                UpdateSearchResults(value);
            }
        }

        void UpdateSearchResults(string value)
        {
            MatchingCustomers.Clear();

            if (String.IsNullOrWhiteSpace(value)) return;

            IsLoading = true;

            foreach (var c in _customerService.GetByName(value))
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
        }

        public ObservableCollection<CustomerViewModel> MatchingCustomers { get; private set; }
    }
}
