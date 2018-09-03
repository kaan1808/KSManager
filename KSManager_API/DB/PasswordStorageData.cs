using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace KSManager_API.DB
{
    public class PasswordStorageData
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public User User { get; set; }

        [Required]
        public string Title { get; set; }

        public int? Icon { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public string Url { get; set; }
        
        public string SecurityQuestion { get; set; }

        public string SecurityAnswer { get; set; }
        
        public string Note { get; set; }
        
        public DateTime LastChanges { get; set; }

        public bool IsDeleted { get; set; }
    }
}
