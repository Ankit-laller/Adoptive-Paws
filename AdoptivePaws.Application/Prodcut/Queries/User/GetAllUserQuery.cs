using AdoptivePaws.Core.Dtos.User;
using AdoptivePaws.Core.Entities;
using MediatR;


namespace AdoptivePaws.Application.Prodcut.Queries.User
{
    public record GetAllUsersQuery() : IRequest<List<UserEntity>>;

    

}
