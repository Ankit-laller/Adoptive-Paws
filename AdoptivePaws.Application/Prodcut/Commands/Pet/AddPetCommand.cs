using AdoptivePaws.Core.Dtos.Pet;
using AdoptivePaws.Core.Dtos.User;
using AdoptivePaws.Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoptivePaws.Application.Prodcut.Commands.Pet
{
    public record AddPetCommand(PetModelDto pet) : IRequest<bool>;

}
