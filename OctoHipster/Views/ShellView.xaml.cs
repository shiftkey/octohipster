using System.Reactive.Linq;
using System.Windows;
using OctoHipster.ViewModels;
using ReactiveUI;

namespace OctoHipster.Views
{
    public partial class ShellView : IViewFor<IShellViewModel>
    {
        public ShellView()
        {
            InitializeComponent();

            // workaround until we get all this looking :gem:
            DataContextChanged += (s, e) => { ViewModel = DataContext as IShellViewModel; };

            this.Bind(ViewModel, vm => vm.SearchText, v => v.SearchText.Text);

            this.OneWayBind(ViewModel, vm => vm.MatchingCustomers, v => v.Items.ItemsSource);

            this.OneWayBind(ViewModel, vm => vm.IsLoading, v => v.progressBar.IsIndeterminate);

            var listHasItemsObs = this.WhenAnyObservable(x => x.ViewModel.MatchingCustomers.CountChanged)
                .Select(x => x > 0);

            var isLoadingObs = this.WhenAnyValue(x => x.ViewModel.IsLoading);

            Observable.CombineLatest(listHasItemsObs, isLoadingObs,
                (hasItems, isLoading) => hasItems && !isLoading)
                .Select(b => b ? Visibility.Visible : Visibility.Collapsed)
                .BindTo(this, v => v.Items.Visibility);
        }

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = value as IShellViewModel; }
        }

        public IShellViewModel ViewModel
        {
            get { return GetValue(ViewModelProperty) as IShellViewModel; }
            set { SetValue(ViewModelProperty, value); }
        }

        public static readonly DependencyProperty ViewModelProperty =
           DependencyProperty.Register(
               "ViewModel",
               typeof(IShellViewModel),
               typeof(ShellView),
               new PropertyMetadata(null));
    }
}
