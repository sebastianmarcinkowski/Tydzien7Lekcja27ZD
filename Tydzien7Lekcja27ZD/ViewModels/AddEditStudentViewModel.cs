using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Tydzien7Lekcja27ZD.Commans;
using Tydzien7Lekcja27ZD.Models.Domains;
using Tydzien7Lekcja27ZD.Models.Wrappers;

namespace Tydzien7Lekcja27ZD.ViewModels
{
    public class AddEditStudentViewModel : BaseViewModel
    {
        private Repository _repository = new Repository();

        public AddEditStudentViewModel(StudentWrapper student = null)
        {
            ConfirmCommand = new RelayCommand(Confirm);
            CloseCommand = new RelayCommand(Close);

            if (student == null)
            {
                Student = new StudentWrapper();
            }
            else
            {
                Student = student;
                IsUpdate = true;
            }

            InitGroups();
        }

        private StudentWrapper _student;

        public StudentWrapper Student
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
            _repository.AddStudent(Student);
        }

        private void UpdateStudent()
        {
            _repository.UpdateStudent(Student);
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
            var groups = _repository.GetGroups();
            groups.Insert(0, new Group { Id = 0, Name = "-- brak --" });

            Groups = new ObservableCollection<Group>(groups);

            Student.Group.Id = Student.Group.Id;
        }
    }
}
