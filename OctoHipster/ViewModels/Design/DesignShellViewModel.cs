namespace OctoHipster.ViewModels.Design
{
    public class DesignShellViewModel : IShellViewModel
    {
        public DesignShellViewModel()
        {
            SearchText = "shiftkey";
        }

        public bool IsLoading { get; private set; }
        public string SearchText { get; set; }
    }
}
