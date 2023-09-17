using Tydzien7Lekcja27ZD.Models.Wrappers;
using Tydzien7Lekcja27ZD.ViewModels;

namespace Tydzien7Lekcja27ZD.Views
{
    public partial class AddEditStudentView
    {
        public AddEditStudentView(StudentWrapper student = null)
        {
            InitializeComponent();
            DataContext = new AddEditStudentViewModel(student);
        }
    }
}
