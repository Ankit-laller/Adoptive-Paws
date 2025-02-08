using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoptivePaws.Core.Common
{
    public class ResponseClass<T>
    {
        public bool success { get; set; }
        public string message { get; set; }
        public string token { get; set; }
        public T  user { get; set; }
        public ResponseClass(T user, bool success = true, string message = "", string token = "")
        {
            this.success = success;
            this.message = message;
            this.token = token;
            this.user = user;
        }
        public ResponseClass(bool success = true, string message = "")
        {
            this.success = success;
            this.message = message;

        }
    }

    public class Response
    {
        public bool success { get; set; }
        public string message { get; set; }
       // public PetModel petData { get; set; }

        public Response(bool success = true, string message = "")
        {
            this.success = success;
            this.message = message;
           // this.petData = petData;
        }
    }
    public class UserResponseDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public string City { get; set; }
    }
}
