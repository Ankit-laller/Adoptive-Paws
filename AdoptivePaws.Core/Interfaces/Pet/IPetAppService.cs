using AdoptivePaws.Core.Dtos.Pet;
using AdoptivePaws.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoptivePaws.Core.Interfaces.Pet
{
    public interface IPetAppService
    {
        Task<bool> AddPetDataAsync(PetModelDto pet);
        Task<PetEntity> GetPetDataByIdAsync(string id);
        Task<List<PetEntity>> GetPetDataByPetTypeAsync(string pet);
        Task<List<PetEntity>> GetAllPets();
        

    }
}
