using System;
using System.Collections.Generic;
using System.Windows;
using Caliburn.Micro;
using OctoHipster.Services;
using OctoHipster.ViewModels;

namespace OctoHipster
{
    public partial class App
    {
        public App()
        {
            InitializeComponent();
        }
    }

    public class AppBootstrapper : BootstrapperBase
    {
        readonly SimpleContainer _container = new SimpleContainer();

        public AppBootstrapper()
        {
            Initialize();
        }

        protected override object GetInstance(Type serviceType, string key)
        {
            return _container.GetInstance(serviceType, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type serviceType)
        {
            return _container.GetAllInstances(serviceType);
        }

        protected override void Configure()
        {
            _container.RegisterSingleton(typeof(IWindowManager), null, typeof(WindowManager));
            _container.RegisterSingleton(typeof(ICustomerService), null, typeof(CustomerService));
            _container.RegisterSingleton(typeof(IShellViewModel), null, typeof(ShellViewModel));

            base.Configure();
        }

        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<IShellViewModel>();
        }
    }
}
