using AdoptivePaws.Core.Common;
using AdoptivePaws.Core.Dtos.Pet;
using AdoptivePaws.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoptivePaws.Core.Interfaces.Pet
{
    public interface IPetReposistory
    {
        Task<List<PetEntity>> getAlPetsDataAsync();
        Task<bool> savePetDataAsync(PetEntity pet);
        Task<PetEntity> getPetDataByIdAsync(String id);
        Task<PetEntity> updatePetDataAsync(String id, PetModelDto value);

        Task<bool> deletePetDataAsync(String id);
    }
}
