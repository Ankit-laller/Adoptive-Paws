using AdoptivePaws.Application.Prodcut.Queries.User;
using AdoptivePaws.Application.Services.User;
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
    public class GetUserByIdHandler(IUserAppService userRepository)
        : IRequestHandler<GetUserByIdQuery, UserDto>
    {
            public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
            {
                return await userRepository.GetUserByIdAsync(request.id);
            }
    }
    
}
