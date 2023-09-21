using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Tydzien7Lekcja27ZD.Commands;
using Tydzien7Lekcja27ZD.Models.Domains;
using Tydzien7Lekcja27ZD.Models.Wrappers;
using Tydzien7Lekcja27ZD.Properties;
using Tydzien7Lekcja27ZD.Views;

namespace Tydzien7Lekcja27ZD.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private Repository _repository = new Repository();

        public static bool IsDBSettingsChanged;

        public MainViewModel()
        {
            ConnectionStringTest();

            AddStudentCommand = new RelayCommand(AddEditStudent);
            EditStudentCommand = new RelayCommand(AddEditStudent, CanEditDeleteStudent);
            DeleteStudentCommand = new AsyncRelayCommand(DeleteStudent, CanEditDeleteStudent);
            RefreshStudentsCommand = new RelayCommand(RefreshStudents);
            DBSettingsCommand = new RelayCommand(DBSettings);

            RefreshDiary();
            InitGroups();
        }

        public ICommand RefreshStudentsCommand { get; set; }
        public ICommand AddStudentCommand { get; set; }
        public ICommand EditStudentCommand { get; set; }
        public ICommand DeleteStudentCommand { get; set; }
        public ICommand DBSettingsCommand { get; set; }

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

        private void ConnectionStringTest()
        {
            string testConnectionString =
                $"Server={Settings.Default.DBServerName}\\{Settings.Default.DBInstanceName};" +
                $"Database={Settings.Default.DBName};" +
                $"User Id={Settings.Default.DBUser};" +
                $"Password={Settings.Default.DBPassword}";

            try
            {
                using (SqlConnection testConnection = new SqlConnection(testConnectionString))
                {
                    testConnection.Open();
                }
            }
            catch (System.Data.SqlClient.SqlException)
            {
                if (Task.Run(() =>
                        MessageBox.Show(
                            "Nie można połączyć się z bazą danych, czy chcesz teraz poprawić ustawienia?",
                            "Brak połączenia z bazą danych!",
                            MessageBoxButton.OKCancel,
                            MessageBoxImage.Error
                        )).Result == MessageBoxResult.Cancel)
                {
                    Environment.Exit(1);
                }

                var dbSettingsWindow = new DBSettingsView();

                dbSettingsWindow.Closed += DbSettingsWindowConfigurationError_Closed;

                dbSettingsWindow.ShowDialog();

                dbSettingsWindow.Closed -= DbSettingsWindowConfigurationError_Closed;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private async void DbSettingsWindowConfigurationError_Closed(object sender, EventArgs e)
        {
            if (IsDBSettingsChanged)
            {
                var metroWindow = Application.Current.MainWindow as MetroWindow;
                await metroWindow.ShowMessageAsync(
                    "Ustawienia zostały zapisane",
                    $"Aplikacja zostanie uruchomiona ponownie.",
                    MessageDialogStyle.Affirmative
                );

                System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
                Application.Current.Shutdown();
            }
            else
            {
                Environment.Exit(1);
            }
        }


        private void DBSettings(object obj)
        {
            var dbSettingsWindow = new DBSettingsView();

            dbSettingsWindow.Closed += DbSettingsWindow_Closed;

            dbSettingsWindow.ShowDialog();

            dbSettingsWindow.Closed -= DbSettingsWindow_Closed;

        }

        private async void DbSettingsWindow_Closed(object sender, EventArgs e)
        {
            if (IsDBSettingsChanged)
            {
                var metroWindow = Application.Current.MainWindow as MetroWindow;
                await metroWindow.ShowMessageAsync(
                    "Ustawienia zostały zapisane",
                    $"Aplikacja zostanie uruchomiona ponownie.",
                    MessageDialogStyle.Affirmative
                );

                System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
                Application.Current.Shutdown();
            }
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

        private void AddEditStudent(object obj)
        {
            var addEditStudentWindow = new AddEditStudentView(obj as StudentWrapper);
            addEditStudentWindow.Closed += AddEditStudentWindowOnClosed;

            addEditStudentWindow.ShowDialog();

            addEditStudentWindow.Closed -= AddEditStudentWindowOnClosed;
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
