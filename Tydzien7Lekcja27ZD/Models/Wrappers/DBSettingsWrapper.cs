using System.ComponentModel;

namespace Tydzien7Lekcja27ZD.Models.Wrappers
{
    public class DBSettingsWrapper : IDataErrorInfo
    {
        public string DBServerName { get; set; }
        public string DBInstanceName { get; set; }
        public string DBName { get; set; }
        public string DBUser { get; set; }
        public string DBPassword { get; set; }


        private bool _isDBServerNameValid;
        private bool _isDBName;
        private bool _isDBUser;
        private bool _isDBPassword;

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case nameof(DBServerName):
                        if (string.IsNullOrWhiteSpace(DBServerName))
                        {
                            Error = "Pole adres serwera jest wymagane.";
                            _isDBServerNameValid = false;
                        }
                        else
                        {
                            Error = string.Empty;
                            _isDBServerNameValid = true;
                        }
                        break;
                    case nameof(DBName):
                        if (string.IsNullOrWhiteSpace(DBName))
                        {
                            Error = "Pole nazwa bazy danych jest wymagane.";
                            _isDBName = false;
                        }
                        else
                        {
                            Error = string.Empty;
                            _isDBName = true;
                        }
                        break;
                    case nameof(DBUser):
                        if (string.IsNullOrWhiteSpace(DBUser))
                        {
                            Error = "Pole użytkownik jest wymagane.";
                            _isDBUser = false;
                        }
                        else
                        {
                            Error = string.Empty;
                            _isDBUser = true;
                        }
                        break;
                    case nameof(DBPassword):
                        if (string.IsNullOrWhiteSpace(DBPassword))
                        {
                            Error = "Pole hasło jest wymagane.";
                            _isDBPassword = false;
                        }
                        else
                        {
                            Error = string.Empty;
                            _isDBPassword = true;
                        }
                        break;
                    default:
                        break;
                }

                return Error;
            }
        }
        public string Error { get; set; }

        public bool IsValid
        {
            get
            {
                return _isDBServerNameValid && _isDBName && _isDBUser && _isDBPassword;
            }
        }
    }
}
