using Tydzien7Lekcja27ZD.ViewModels;

namespace Tydzien7Lekcja27ZD.Views
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }
    }
}
