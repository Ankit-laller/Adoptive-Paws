using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoptivePaws.Application.Prodcut.Commands.Pet
{
    public record RejectAdoptionRequestCommand(string input) :IRequest<string>
    {
    }
}
