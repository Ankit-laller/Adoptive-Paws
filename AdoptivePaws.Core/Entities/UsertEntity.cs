using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoptivePaws.Core.Entities
{
    public class UserEntity
    {
        [Key]
        public int SNo { get; set; }
        [MaxLength(50)]
        public string Name { get; set; } = null!;
        [MaxLength(50)]
        [EmailAddress]
        public string Email { get; set; } = null!;
        [MaxLength(12)]
        public string PhoneNo { get; set; } = null!;
        //[MaxLength(12)]
        public string PasswordHash { get; set; } = null!;
        public string PasswordSalt { get; set; } = null!;
        [MaxLength(50)]
        public DateTime CreatedOn { get; set; }= DateTime.Now;
        [MaxLength(50)]
        public DateTime UpdatedOn { get; set;} = DateTime.Now;
        public bool IsDeleted { get; set; } = false;
        [MaxLength(1000)]
        public string UpdatedBy { get; set; } = "1";
        [MaxLength(1000)]
        public int TenantId { get; set; } = 0;
        public bool IsTenant { get; set; } = false;
        [MaxLength(50)]
        public string City { get; set;} = null!;

    }
}
