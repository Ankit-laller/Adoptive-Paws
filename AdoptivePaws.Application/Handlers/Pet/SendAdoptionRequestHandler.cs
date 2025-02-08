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
    public class SendAdoptionRequestHandler : IRequestHandler<SendAdoptionRequestCommand, string>
    {
        private readonly IAdoptionAppService _adoptionAppService;
        public SendAdoptionRequestHandler(IAdoptionAppService adoptionAppService)
        {
            _adoptionAppService = adoptionAppService;
        }
        public async Task<string> Handle(SendAdoptionRequestCommand request, CancellationToken cancellationToken)
        {
            return await _adoptionAppService.SendAdoptionRequest(request.input);
        }
    }
}
