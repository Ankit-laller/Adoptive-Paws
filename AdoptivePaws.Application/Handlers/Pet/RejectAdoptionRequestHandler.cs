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
    public class RejectAdoptionRequestHandler : IRequestHandler<RejectAdoptionRequestCommand, string>
    {
        private readonly IAdoptionAppService _adoptionAppService;
        public RejectAdoptionRequestHandler(IAdoptionAppService adoptionAppService)
        {
            _adoptionAppService = adoptionAppService;
        }
        public async Task<string> Handle(RejectAdoptionRequestCommand request, CancellationToken cancellationToken)
        {
            return await _adoptionAppService.DeleteAdoptionRequest(request.input);
        }
    }
}
