using System;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Input;
using Tydzien7Lekcja27ZD.Commands;
using Tydzien7Lekcja27ZD.Models.Wrappers;
using Tydzien7Lekcja27ZD.Properties;

namespace Tydzien7Lekcja27ZD.ViewModels
{
    class DBSettingsViewModel : BaseViewModel
    {
        public DBSettingsViewModel()
        {
            _dbSettings.DBServerName = Settings.Default.DBServerName;
            _dbSettings.DBInstanceName = Settings.Default.DBInstanceName;
            _dbSettings.DBName = Settings.Default.DBName;
            _dbSettings.DBUser = Settings.Default.DBUser;
            _dbSettings.DBPassword = Settings.Default.DBPassword;

            ConfirmCommand = new RelayCommand(Confirm, CanConfirm);
            CloseCommand = new RelayCommand(Close);
        }

        private DBSettingsWrapper _dbSettings = new DBSettingsWrapper();

        public DBSettingsWrapper DBSettings
        {
            get { return _dbSettings; }
            set
            {
                _dbSettings = value;
                OnPropertyChanged();
            }
        }

        public ICommand ConfirmCommand { get; set; }
        public ICommand CloseCommand { get; set; }

        private void Confirm(object obj)
        {
            if (!ConnectionStringTest())
                return;

            Settings.Default.DBServerName = _dbSettings.DBServerName;
            Settings.Default.DBInstanceName = _dbSettings.DBInstanceName;
            Settings.Default.DBName = _dbSettings.DBName;
            Settings.Default.DBUser = _dbSettings.DBUser;
            Settings.Default.DBPassword = _dbSettings.DBPassword;

            Settings.Default.Save();

            MainViewModel.IsDBSettingsChanged = true;

            CloseWindow(obj as Window);

        }

        private bool CanConfirm(object obj)
        {
            return _dbSettings.IsValid;
        }

        private bool ConnectionStringTest()
        {
            string testConnectionString =
                $"Server={_dbSettings.DBServerName}\\{_dbSettings.DBInstanceName};" +
                $"Database={_dbSettings.DBName};" +
                $"User Id={_dbSettings.DBUser};" +
                $"Password={_dbSettings.DBPassword}";

            try
            {
                using (SqlConnection testConnection = new SqlConnection(testConnectionString))
                {
                    testConnection.Open();
                }
            }
            catch (System.Data.SqlClient.SqlException)
            {
                MessageBox.Show("Podałeś niepoprawne dane, spróbuj jeszcze raz!");
                return false;
            }
            catch (Exception e)
            {
                throw e;
            }

            return true;
        }

        private void Close(object obj)
        {
            CloseWindow(obj as Window);
        }

        private void CloseWindow(Window window)
        {
            window.Close();
        }
    }
}
