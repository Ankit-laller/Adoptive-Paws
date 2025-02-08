using AdoptivePaws.Application.Events;
using AdoptivePaws.Core.Dtos.User;
using AdoptivePaws.Core.Entities;
using AdoptivePaws.Core.Interfaces;
using MediatR;


namespace AdoptivePaws.Application.Prodcut.Commands
{
    public record AddUserCommand(AddUserDto user) : IRequest<UserDto>;

    

}
