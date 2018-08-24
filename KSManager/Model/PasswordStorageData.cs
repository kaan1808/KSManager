using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSManager.Model
{
    public class PasswordStorageData
    {
        public string Title { get; set; }

        public byte[] Icon { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public string Url { get; set; }

        public string SecurityQuestion { get; set; }

        public string SecurityAnswer { get; set; }

        public string Note { get; set; }

        public DateTime LastChanges { get; set; }
    }
}
