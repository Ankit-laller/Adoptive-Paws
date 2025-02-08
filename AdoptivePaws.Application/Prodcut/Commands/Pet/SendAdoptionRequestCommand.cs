using AdoptivePaws.Core.Dtos.Pet;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoptivePaws.Application.Prodcut.Commands.Pet
{
    public record SendAdoptionRequestCommand(AdoptionRequestDto input) :IRequest<string>
    {
    }
}
