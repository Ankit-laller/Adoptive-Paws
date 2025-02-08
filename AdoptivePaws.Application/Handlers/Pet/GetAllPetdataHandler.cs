using AdoptivePaws.Application.Prodcut.Queries.Pet;
using AdoptivePaws.Core.Entities;
using AdoptivePaws.Core.Interfaces.Pet;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoptivePaws.Application.Handlers.Pet
{
    public class GetAllPetdataHandler(IPetAppService petAppService)
        : IRequestHandler<GetAllPetsDataQuery, List<PetEntity>>
    {
        public async Task<List<PetEntity>> Handle(GetAllPetsDataQuery request, CancellationToken cancellationToken)
        {
            return await petAppService.GetAllPets();
        }
    }
}
