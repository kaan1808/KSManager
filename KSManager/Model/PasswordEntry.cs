using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace KSManager.Model
{
    public class PasswordEntry
        : PropertyChangedBase
    {
        private Guid _id;
        private string _title;
        private string _username;
        private byte[] _icon;
        private string _password;
        private string _email;
        private string _url;
        private string _securityquestion;
        private string _securityanswer;
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
        public byte[] Icon
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

        public string Securityquestion
        {
            get => _securityquestion;
            set => Set(ref _securityquestion, value);
        }

        public string Securityanswer
        {
            get => _securityanswer;
            set => Set(ref _securityanswer, value);
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
    }
}
