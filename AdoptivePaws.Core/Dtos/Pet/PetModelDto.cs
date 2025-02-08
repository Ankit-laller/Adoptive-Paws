using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoptivePaws.Core.Dtos.Pet
{
    public class PetModelDto
    {
        public string petName { get; set; }
        public string description { get; set; }
        public int petAge { get; set; }
        public string petGender { get; set; }
        public bool vaccinated { get; set; } = false;

        public string petType { get; set; }
        public string? userId { get; set; }
        public int price { get; set; }
        public string address { get; set; }
        [FromForm]
        public IFormFileCollection Image { get; set; }

    }
}
