using System;

namespace KSManager.Model
{
    public class User
    {
        public Guid Id { get; set; }

        public string UserName { get; set; }
        public byte[] Password { get; set; }
        public byte[] Salt { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
    }
}