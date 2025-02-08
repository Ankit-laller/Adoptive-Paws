using AdoptivePaws.Core.Common;
using AdoptivePaws.Core.Dtos.Pet;
using AdoptivePaws.Core.Entities;
using AdoptivePaws.Core.Interfaces.Pet;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace AdoptivePaws.Application.Services.Pet
{
    public class PetAppService : IPetAppService
    {
        private readonly IPetReposistory _petReposistory;
        private readonly ICommonAppService _commonAppService;
        public PetAppService(IPetReposistory petReposistory, ICommonAppService commonAppService)
        {
            _petReposistory = petReposistory;
            _commonAppService = commonAppService;
        }

        #region[AddPet]
        public async Task<bool> AddPetDataAsync(PetModelDto value) 
        { 
             Guid myuuid = Guid.NewGuid();
                PetEntity newPet = new PetEntity()
                {
                    petId = myuuid.ToString(),
                    petName = value.petName,
                    description = value.description,
                    userId = value.userId??"1",
                    petType = value.petType,
                    petAge = value.petAge,
                    // petColor = value.petColor,
                    petGender = value.petGender,
                    petImages = new List<Image>(),
                    address = value.address,
                    vaccinated = value.vaccinated,
                    price = value.price,
                };
            //uploading image on blob
            if (value.Image.Count > 0)
            {
                foreach (var formFile in value.Image)
                {
                    if (formFile.Length > 0)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await formFile.CopyToAsync(memoryStream);
                            // Upload the file if less than 6 MB  
                            if (memoryStream.Length < 6291456)
                            {
                                //for using blob storage
                                /*var result = await _commonAppService.UploadFileToBlobAsync(formFile, "images");
                                Image image = new Image()
                                {
                                    CreatedOn = DateTime.Now,
                                    ImageName = result.ImageName,
                                    ImageUrl = result.ImageUrl,
                                    IsUploaded = true,
                                };*/
                                Image image = new Image()
                                {
                                    CreatedOn = DateTime.Now,
                                    ImageName = "result.ImageName",
                                    ImageUrl = "result.ImageUrl",
                                    IsUploaded = true,
                                };
                                newPet.petImages.Add(image);
                            }
                            else
                            {
                                Image image = new Image()
                                {
                                    CreatedOn = DateTime.Now,
                                    ImageName = "",
                                    ImageUrl = "",
                                    ErrorMessage= "image greater than 6 MB",
                                   IsUploaded= false,
                                };
                                newPet.petImages.Add(image);
                            }
                        }
                    }
                }
            }
            return await _petReposistory.savePetDataAsync(newPet);
        }

       
        #endregion

        public Task<PetEntity> GetPetDataByIdAsync(string id)
        {
            return _petReposistory.getPetDataByIdAsync(id);
        }

        public async Task<List<PetEntity>> GetPetDataByPetTypeAsync(string pet)
        {
            return (await _petReposistory.getAlPetsDataAsync()).Where(p=>p.petType.ToLower() == pet.ToLower()).ToList();
        }

        public async Task<List<PetEntity>> GetAllPets()
        {
            return (await _petReposistory.getAlPetsDataAsync()).Where(p=>p.isAdopted==false).ToList();
        }

    }
}
