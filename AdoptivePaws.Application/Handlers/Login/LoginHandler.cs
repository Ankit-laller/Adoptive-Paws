using AdoptivePaws.Application.Events;
using AdoptivePaws.Application.Prodcut.Queries.Login;
using AdoptivePaws.Core.Dtos;
using AdoptivePaws.Core.Entities;
using AdoptivePaws.Core.Interfaces;
using AdoptivePaws.Core.Interfaces.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoptivePaws.Application.Handlers.Login
{

    public class LoginHandler(ILoginAppService loginAppService, IPublisher mediator) : IRequestHandler<LoginQuery, LoginReponse>
    {
        public async Task<LoginReponse> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var user = await loginAppService.LoginAsync(request.user);
           // await mediator.Publish(new UserCreatedEvent());
            return user;
        }

    }
}
