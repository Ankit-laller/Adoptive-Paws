using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace AdoptivePaws.Core.Entities
{
    public class PetEntity
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SNo { get; set; }
        public string petId { get; set; }
        public string petName { get; set; }
        public string description { get; set; }
        public int petAge { get; set; }
        public bool isAdopted { get; set; } = false;
        public bool vaccinated { get; set; }
        public string petGender { get; set; }
        public string petType { get; set; }
        public string userId { get; set; }
        public int price { get; set; }
        public string address { get; set; }
        public List<Image> petImages { get; set; }
        public DateTime CreatedOn { get; set; }
    }
    public class Image
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public bool IsUploaded { get; set; }
        public string ErrorMessage { get; set; }
        public string ImageUrl { get; set; }
        public string ImageName { get; set; }
        public DateTime CreatedOn { get; set; }
        //public int PetEntitySno { get; set; }


    }
}
