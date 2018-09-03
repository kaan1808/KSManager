using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using KSManager.Helper;

namespace KSManager.Model
{
    public class PasswordEntry
        : PropertyChangedBase, ICloneable
    {
        private Guid _id;
        private string _title;
        private string _username;
        private int _icon;
        private string _password;
        private string _email;
        private string _url;
        private string _securityQuestion;
        private string _securityAnswer;
        private string _note;
        private DateTime _lastChanges;

        public Guid Id
        {
            get => _id;
            set => Set(ref _id, value);
        }

        public string Title
        {
            get => _title;
            set => Set(ref _title, value);
        }

        public string Username
        {
            get => _username;
            set => Set(ref _username, value);
        }
        public int Icon
        {
            get => _icon;
            set => Set(ref _icon, value);
        }

        public string Password
        {
            get => _password; 
            set => Set(ref _password, value);
        }
        public string Email
        {
            get => _email; 
            set => Set(ref _email, value);
        }

        public string Url
        {
            get => _url;
            set => Set(ref _url, value);
        }

        public string SecurityQuestion
        {
            get => _securityQuestion;
            set => Set(ref _securityQuestion, value);
        }

        public string SecurityAnswer
        {
            get => _securityAnswer;
            set => Set(ref _securityAnswer, value);
        }

        public string Note
        {
            get => _note;
            set => Set(ref _note, value);
        }

        public DateTime LastChanges
        {
            get => _lastChanges;
            set => Set(ref _lastChanges, value);
        }

        public object Clone()
        {
            var copy = new PasswordEntry();
            copy.Email = Email.CopyIfNotNull();
            copy.Icon = Icon;
            copy.Id = Id;
            copy.LastChanges = LastChanges;
            copy.Note = Note;
            copy.Password = Password;
            copy.SecurityAnswer = SecurityAnswer;
            copy.SecurityQuestion = SecurityQuestion;
            copy.Title = Title;
            copy.Username = Username;
            copy.Url = Url;

            return copy;
        }
    }
}
