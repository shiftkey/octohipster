using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace OctoHipster.ViewModels
{
    public class OrderViewModel : ViewModelBase
    {
        private OrderItemViewModel _selectedItem;

        public OrderViewModel()
        {
            Items = new ObservableCollection<OrderItemViewModel>();

            AddItemCommand = new RelayCommand(AddItem);
            RemoveItemCommand = new RelayCommand<OrderItemViewModel>(RemoveItem, CanRemoveItem);
        }

        public OrderItemViewModel SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                if (_selectedItem == value) return;
                _selectedItem = value;
                RaisePropertyChanged(() => SelectedItem);
            }
        }

        public ObservableCollection<OrderItemViewModel> Items { get; set; }

        public RelayCommand AddItemCommand { get; set; }

        public void AddItem()
        {
            Items.Add(new OrderItemViewModel());
        }
        public RelayCommand<OrderItemViewModel> RemoveItemCommand { get; set; }

        bool CanRemoveItem(OrderItemViewModel arg)
        {
            return SelectedItem != null;
        }

        public void RemoveItem(OrderItemViewModel viewModel)
        {
            if (Items.Contains(viewModel))
            {
                Items.Remove(viewModel);
            }
        }
    }
}