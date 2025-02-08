using AdoptivePaws.Application.Prodcut.Commands.Pet;
using AdoptivePaws.Core.Interfaces.Pet;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoptivePaws.Application.Handlers.Pet
{
    public class AddPetHandler : IRequestHandler<AddPetCommand,bool>
    {
        private readonly IPetAppService _petAppService;

        public AddPetHandler(IPetAppService petAppService)
        {
            _petAppService = petAppService;
        }

        public async Task<bool> Handle(AddPetCommand request, CancellationToken cancellationToken)
        {
            return await _petAppService.AddPetDataAsync(request.pet);
        }
    }
}
