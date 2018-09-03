using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace KSManager.Model
{
    public class SmallPasswordEntry
        : PropertyChangedBase
    {
        private Guid _id;
        private string _title;
        private string _username;
        private int _icon;

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
    }
}
