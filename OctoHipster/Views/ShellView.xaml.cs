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
