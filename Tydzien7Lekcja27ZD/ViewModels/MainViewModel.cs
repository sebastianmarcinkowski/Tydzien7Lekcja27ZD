using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Tydzien7Lekcja27ZD.Commans;
using Tydzien7Lekcja27ZD.Models.Domains;
using Tydzien7Lekcja27ZD.Models.Wrappers;
using Tydzien7Lekcja27ZD.Views;

namespace Tydzien7Lekcja27ZD.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private Repository _repository = new Repository();

        public MainViewModel()
        {
            AddStudentCommand = new RelayCommand(AddEditStudent);
            EditStudentCommand = new RelayCommand(AddEditStudent, CanEditDeleteStudent);
            DeleteStudentCommand = new AsyncRelayCommand(DeleteStudent, CanEditDeleteStudent);
            RefreshStudentsCommand = new RelayCommand(RefreshStudents);

            RefreshDiary();
            InitGroups();
        }

        public ICommand RefreshStudentsCommand { get; set; }
        public ICommand AddStudentCommand { get; set; }
        public ICommand EditStudentCommand { get; set; }
        public ICommand DeleteStudentCommand { get; set; }

        private StudentWrapper _selectedStudent;

        public StudentWrapper SelectedStudent
        {
            get { return _selectedStudent; }
            set
            {
                _selectedStudent = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<StudentWrapper> _students;

        public ObservableCollection<StudentWrapper> Students
        {
            get { return _students; }
            set
            {
                _students = value;
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

        private void AddEditStudent(object obj)
        {
            var addEditStudentWindow = new AddEditStudentView(obj as StudentWrapper);
            addEditStudentWindow.Closed += AddEditStudentWindowOnClosed;

            addEditStudentWindow.ShowDialog();

            addEditStudentWindow.Closed -= AddEditStudentWindowOnClosed;
        }

        private void AddEditStudentWindowOnClosed(object sender, EventArgs e)
        {
            RefreshDiary();
        }

        private async Task DeleteStudent(object obj)
        {
            var metroWindow = Application.Current.MainWindow as MetroWindow;
            var dialog = await metroWindow.ShowMessageAsync(
                "Usuwanie ucznia",
                $"Czy na pewno chcesz usunąć ucznia {SelectedStudent.FirstName} {SelectedStudent.LastName}?",
                MessageDialogStyle.AffirmativeAndNegative
                );

            if (dialog != MessageDialogResult.Affirmative)
                return;

            _repository.DeleteStudent(SelectedStudent.Id);

            RefreshDiary();
        }

        private bool CanEditDeleteStudent(object obj)
        {
            return SelectedStudent != null;
        }

        private void RefreshStudents(object obj)
        {
            RefreshDiary();
        }

        private void RefreshDiary()
        {
            Students = new ObservableCollection<StudentWrapper>(_repository.GetStudents(SelectedGroupId));
        }

        private void InitGroups()
        {
            var groups = _repository.GetGroups();
            groups.Insert(0, new Group { Id = 0, Name = "Wszystkie" });

            Groups = new ObservableCollection<Group>(groups);

            SelectedGroupId = 0;
        }
    }
}
