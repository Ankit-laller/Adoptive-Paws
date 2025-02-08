using AdoptivePaws.Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoptivePaws.Application.Prodcut.Queries.Pet
{
    public record GetAllPetsDataQuery : IRequest<List<PetEntity>>;
    
}
