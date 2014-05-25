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
   
            var viewModel = new ShellViewModel(customerService);

            var view = new NewShellView { DataContext = viewModel };

            mainWindow.Content = view;
            mainWindow.Show();
        }
    }
}
