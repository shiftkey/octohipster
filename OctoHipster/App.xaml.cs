using System.Windows;
using OctoHipster.Models;
using OctoHipster.Services;
using OctoHipster.ViewModels;
using OctoHipster.Views;

namespace OctoHipster
{
    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var mainWindow = new MainWindow();

            var customerService = new CustomerService();
            var orderService = new OrderService();

            var viewModel = new ShellViewModel(customerService, orderService);
            viewModel.Activate();

            var view = new ShellView { DataContext = viewModel };

            mainWindow.Content = view;
            mainWindow.Show();
        }
    }
}
