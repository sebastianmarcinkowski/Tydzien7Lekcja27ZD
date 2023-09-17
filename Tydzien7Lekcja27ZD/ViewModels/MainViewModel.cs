using System.Windows;
using System.Windows.Input;
using Tydzien7Lekcja27ZD.Commans;

namespace Tydzien7Lekcja27ZD.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public MainViewModel()
        {
            RefreshStudentCommand = new RelayCommand(RefreshStudents, CanRefreshStudents);
        }

        public ICommand RefreshStudentCommand { get; set; }

        private void RefreshStudents(object obj)
        {
            MessageBox.Show("RefreshStudents");
        }

        private bool CanRefreshStudents(object obj)
        {
            return true;
        }
    }
}
