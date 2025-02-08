using AdoptivePaws.Application.Events;
using AdoptivePaws.Application.Prodcut.Commands;
using AdoptivePaws.Core.Dtos.User;
using AdoptivePaws.Core.Entities;
using AdoptivePaws.Core.Interfaces.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoptivePaws.Application.Handlers.User
{
    public class AddUserHandler(IUserAppService userAppService, IPublisher mediator) :
        IRequestHandler<AddUserCommand, UserDto>
    {
        public async Task<UserDto> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var user = await userAppService.AddUserAsync(request.user);
           // await mediator.Publish(new UserCreatedEvent());
            return user;
        }
    }
}
