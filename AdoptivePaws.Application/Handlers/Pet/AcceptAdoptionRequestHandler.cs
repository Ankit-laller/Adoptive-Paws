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
    public class AcceptAdoptionRequestHandler : IRequestHandler<AcceptAdoptionRequestCommand, bool>
    {
        private readonly IAdoptionAppService _adoptionAppService;
        public AcceptAdoptionRequestHandler(IAdoptionAppService adoptionAppService)
        {
            _adoptionAppService = adoptionAppService;
        }
        public async Task<bool> Handle(AcceptAdoptionRequestCommand request, CancellationToken cancellationToken)
        {
            return await _adoptionAppService.AcceptAdoptionRequest(request.input);
        }
    }
}
