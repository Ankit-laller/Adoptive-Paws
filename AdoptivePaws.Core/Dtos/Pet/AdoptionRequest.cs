using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoptivePaws.Core.Dtos.Pet
{
    public class AdoptionRequest
    {
        public string requestId { get; set; }
        public bool status { get; set; } = false;
        public string senderId { get; set; }
        public string senderName { get; set; }
        public string ownerId { get; set; }
        public string petId { get; set; }
        public string petName { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime AcceptedOn { get; set; }

    }
    public class AdoptionRequestDto
    {
       /* public string? senderId { get; set; } 
        public string? senderName { get; set; }*/
        public string ownerId { get; set; }
        public string petId { get; set; }
        public string petName { get; set; }
    }
}
