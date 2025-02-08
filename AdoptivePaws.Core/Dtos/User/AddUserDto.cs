using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoptivePaws.Core.Dtos.User
{
    public class AddUserDto
    {

        public int? Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = null!;
        [Required]
        [MaxLength(50)]
        [EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        [MaxLength(50)]
        public string Password { get; set; } = null!;
        [Required]
        [MaxLength(12)]
        public string PhoneNo { get; set; } = null!;
        [Required]
        [MaxLength(30)]
        public string City { get; set; } = null!;
        //public string TenentId { get; set; }
    }
    public class AddUserDataDto :AddUserDto
    {
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
    }
    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNo { get; set; } = null!;
        public string City { get; set; } = null!;
    }
}
