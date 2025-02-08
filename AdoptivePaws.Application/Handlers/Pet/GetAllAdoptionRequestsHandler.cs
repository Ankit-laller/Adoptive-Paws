using AdoptivePaws.Application.Prodcut.Queries.Pet;
using AdoptivePaws.Application.Services.Pet;
using AdoptivePaws.Core.Dtos.Pet;
using AdoptivePaws.Core.Interfaces.Pet;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoptivePaws.Application.Handlers.Pet
{
    public class GetAllAdoptionRequestsHandler : IRequestHandler<GetAllAdoptionRequestQuery, List<AdoptionRequest>>
    {
        private readonly IAdoptionAppService _adoptionAppService;
        public GetAllAdoptionRequestsHandler(IAdoptionAppService adoptionAppService)
        {
            _adoptionAppService = adoptionAppService;
        }
        public async Task<List<AdoptionRequest>> Handle(GetAllAdoptionRequestQuery request, CancellationToken cancellationToken)
        {
            return await _adoptionAppService.GetAllAdoptionRequest();
        }
    }
}
