using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using OctoHipster.Logic;
using OctoHipster.Services;

namespace OctoHipster.ViewModels
{
    public interface IShellViewModel
    {
        bool IsLoading { get; }
        string SearchText { get; set; }
    }

    public class ShellViewModel : ViewModelBase, IShellViewModel, IActivate
    {
        readonly CustomerService _customerService;

        public ShellViewModel(CustomerService customerService, OrderService orderService)
        {
            _customerService = customerService;

            Customers = new ObservableCollection<CustomerViewModel>();
        }

        public ObservableCollection<CustomerViewModel> Customers { get; set; }

        public void Activate()
        {
            foreach (var c in _customerService.GetAll())
            {
                Customers.Add(new CustomerViewModel
                {
                    Name = c.Name,
                    Company = c.Company,
                    Contact = c.Contact,
                    DateOfBirth = c.DateOfBirth
                });
            }
        }

        public bool IsLoading { get; private set; }

        public string SearchText { get; set; }
    }
}
