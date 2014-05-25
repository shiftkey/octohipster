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

            this.Bind(ViewModel, vm => vm.SearchText, v => v.SearchText.Text);
        }

        object IViewFor.ViewModel
        {
            get { return GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(
            "ViewModel", typeof (IShellViewModel), typeof (ShellView), new PropertyMetadata(default(IShellViewModel)));

        public IShellViewModel ViewModel
        {
            get { return (IShellViewModel) GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }
    }
}
