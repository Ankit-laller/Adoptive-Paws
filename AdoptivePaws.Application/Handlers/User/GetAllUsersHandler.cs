using AdoptivePaws.Application.Prodcut.Queries.User;
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
    public class GetAllUsersHandler(IUserRepository userRepository)
    : IRequestHandler<GetAllUsersQuery, List<UserEntity>>
    {
        public async Task<List<UserEntity>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            return await userRepository.GetAllUsersAsync();
        }
    }
}
