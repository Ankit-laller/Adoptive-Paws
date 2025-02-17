using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoptivePaws.Core.Dtos
{
    public class ApiResponse
    {
        public bool success { get; set; }
        public string message { get; set; }
        public dynamic result { get; set; }
    }
}
