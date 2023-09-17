using Tydzien7Lekcja27ZD.Models;
using Tydzien7Lekcja27ZD.ViewModels;

namespace Tydzien7Lekcja27ZD.Views
{
    public partial class AddEditStudentView
    {
        public AddEditStudentView(Student student  = null)
        {
            InitializeComponent();
            DataContext = new AddEditStudentViewModel(student);
        }
    }
}
