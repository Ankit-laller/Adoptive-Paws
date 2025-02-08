

using AdoptivePaws.Application.Prodcut.Queries.Pet;
using AdoptivePaws.Application.Services.Pet;
using AdoptivePaws.Core.Entities;
using AdoptivePaws.Core.Interfaces.Pet;
using MediatR;

public class GetPetDataByPetTypeHandler : IRequestHandler<GetPetDataByPetTypeQuery, List<PetEntity>>
{
    private readonly IPetAppService _petAppService;
    public GetPetDataByPetTypeHandler(IPetAppService petAppService)
    {
        _petAppService = petAppService;
    }
    public Task<List<PetEntity>> Handle(GetPetDataByPetTypeQuery request, CancellationToken cancellationToken)
    {
        return _petAppService.GetPetDataByPetTypeAsync(request.petType);
    }
}