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
    public class GetPetDataByPetIdHandler(IPetAppService petAppService) : IRequestHandler<GetPetDataByIdQuery, PetEntity>
    {
        public Task<PetEntity> Handle(GetPetDataByIdQuery request, CancellationToken cancellationToken)
        {
            return petAppService.GetPetDataByIdAsync(request.petId);
        }
    }
}
