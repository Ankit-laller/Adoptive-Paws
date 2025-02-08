using AdoptivePaws.Core.Common;
using AdoptivePaws.Core.Dtos.Pet;
using AdoptivePaws.Core.Entities;
using AdoptivePaws.Core.Interfaces.Pet;
using AdoptivePaws.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoptivePaws.Infrastructure.Repositories.Pet
{
    public class PetRepository : IPetReposistory
    {   
        private readonly AppDbContext _appDbContext;
        public PetRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<bool> deletePetDataAsync(string id)
        {
            var pet = await _appDbContext.Pets.FirstOrDefaultAsync(x => x.petId == id);

            if (pet is not null)
            {
                _appDbContext.Pets.Remove(pet);

                return await _appDbContext.SaveChangesAsync() > 0;
            }

            return false;
        }

        public async Task<List<PetEntity>> getAlPetsDataAsync()
        {
            return await _appDbContext.Pets.ToListAsync();
        }

        public async Task<PetEntity> getPetDataByIdAsync(string id)
        {
            return await _appDbContext.Pets.FirstOrDefaultAsync(p=>p.petId == id);
        }

        public async Task<bool> savePetDataAsync(PetEntity value)
        {
            await _appDbContext.Pets.AddAsync(value);
            return await _appDbContext.SaveChangesAsync() >0;
        }

        public async Task<PetEntity> updatePetDataAsync(string id, PetModelDto value)
        {
            var pet = await _appDbContext.Pets.FirstOrDefaultAsync(x=>x.petId==id);
            if (pet != null)
            {
                pet.petName = value.petName;
                pet.petType = value.petType;
                pet.petGender = value.petGender;
                pet.petAge = value.petAge;
                pet.petType = value.petType;
                pet.address = value.address;
                pet.description = value.description;
                pet.price = value.price;
                pet.vaccinated = value.vaccinated;
            }
            await _appDbContext.SaveChangesAsync();
            return pet;
        }
    }
}
