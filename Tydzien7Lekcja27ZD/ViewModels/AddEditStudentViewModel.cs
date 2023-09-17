using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Tydzien7Lekcja27ZD.Commans;
using Tydzien7Lekcja27ZD.Models;

namespace Tydzien7Lekcja27ZD.ViewModels
{
    public class AddEditStudentViewModel : BaseViewModel
    {
        public AddEditStudentViewModel(Student student = null)
        {
            ConfirmCommand = new RelayCommand(Confirm);
            CloseCommand = new RelayCommand(Close);

            if (student == null)
            {
                Student = new Student();
            }
            else
            {
                Student = student;
                IsUpdate = true;
            }

            InitGroups();
        }

        private Student _student;

        public Student Student
        {
            get { return _student; }
            set
            {
                _student = value;
                OnPropertyChanged();
            }
        }

        private bool _isUpdate;

        public bool IsUpdate
        {
            get { return _isUpdate; }
            set
            {
                _isUpdate = value;
                OnPropertyChanged();
            }
        }

        private int _selectedGroupId;

        public int SelectedGroupId
        {
            get { return _selectedGroupId; }
            set
            {
                _selectedGroupId = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Group> _groups;

        public ObservableCollection<Group> Groups
        {
            get { return _groups; }
            set
            {
                _groups = value;
                OnPropertyChanged();
            }
        }

        public ICommand ConfirmCommand { get; set; }
        public ICommand CloseCommand { get; set; }

        private void Confirm(object obj)
        {
            if (!IsUpdate)
                AddStudent();
            else
                UpdateStudent();

            CloseWindow(obj as Window);
        }

        private void AddStudent()
        {
            // DB.
        }

        private void UpdateStudent()
        {
            // DB.
        }

        private void Close(object obj)
        {
            CloseWindow(obj as Window);
        }

        private void CloseWindow(Window window)
        {
            window.Close();
        }

        private void InitGroups()
        {
            Groups = new ObservableCollection<Group>
            {
                new Group { Id = 0, Name = "-- brak --" },
                new Group { Id = 1, Name = "1A" },
                new Group { Id = 2, Name = "2A" }
            };

            Student.Group.Id = 0;
        }
    }
}
