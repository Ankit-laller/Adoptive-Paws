using AdoptivePaws.Application.Prodcut.Queries.Login;
using AdoptivePaws.Core.Dtos;
using AdoptivePaws.Core.Interfaces.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoptivePaws.Application.Handlers.Login
{
    public class ValidateTokenHandler(ILoginAppService loginAppService) : IRequestHandler<ValidateTokenQuery, LoginReponse>
    {
        public async Task<LoginReponse> Handle(ValidateTokenQuery request, CancellationToken cancellationToken)
        {
            return await loginAppService.ValidateTokenAsync(request.token);
        }
    }
}
