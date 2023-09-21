using Tydzien7Lekcja27ZD.Models.Wrappers;
using Tydzien7Lekcja27ZD.ViewModels;

namespace Tydzien7Lekcja27ZD.Views
{
    public partial class DBSettingsView
    {
        public DBSettingsView()
        {
            InitializeComponent();
            DataContext = new DBSettingsViewModel();
        }
    }
}
