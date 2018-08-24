using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace KSManager_API.DB
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [StringLength(64)]
        public string Username { get; set; }

        [Required]
        [MaxLength(64)]
        public byte[] Password { get; set; }

        [Required]
        [MaxLength(32)]
        public byte[] Salt { get; set; }

        public int Iterations { get; set; }

        [StringLength(64)]
        public string FirstName { get; set; }

        [StringLength(64)]
        public string LastName { get; set; }

        public DateTime? Birthday { get; set; }

        public virtual ICollection<PasswordStorageData> PasswordStorageDatas { get; set; }
    }
}
