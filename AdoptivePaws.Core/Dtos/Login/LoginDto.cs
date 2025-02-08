using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoptivePaws.Core.Dtos
{
    public class LoginDto
    {
        [MaxLength(50)]
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [MaxLength(50)]
        [Required]
        public string Password { get; set; }
    }
    public class LoginReponse
    {
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string token { get; set; }
        public string PhoneNo { get; set; }
        public string City { get; set; }
    }
    }
