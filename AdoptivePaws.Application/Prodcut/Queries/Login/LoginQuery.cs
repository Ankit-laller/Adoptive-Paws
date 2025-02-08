using AdoptivePaws.Core.Dtos;
using AdoptivePaws.Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoptivePaws.Application.Prodcut.Queries.Login
{
    public record LoginQuery(LoginDto user) : IRequest<LoginReponse>;
}
