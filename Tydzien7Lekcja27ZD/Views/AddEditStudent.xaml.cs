using Tydzien7Lekcja27ZD.ViewModels;

namespace Tydzien7Lekcja27ZD.Views
{
    public partial class AddEditStudent
    {
        public AddEditStudent()
        {
            InitializeComponent();
            DataContext = new AddEditStudentViewModel();
        }
    }
}
