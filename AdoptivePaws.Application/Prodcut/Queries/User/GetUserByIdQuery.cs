using AdoptivePaws.Core.Dtos.User;
using AdoptivePaws.Core.Entities;
using AdoptivePaws.Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoptivePaws.Application.Prodcut.Queries.User
{
    public record GetUserByIdQuery(int id) : IRequest<UserDto>;

    
}
