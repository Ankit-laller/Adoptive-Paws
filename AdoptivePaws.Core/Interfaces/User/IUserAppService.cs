using AdoptivePaws.Core.Dtos.User;
using AdoptivePaws.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoptivePaws.Core.Interfaces.User
{
    public interface IUserAppService
    {
        Task<UserDto> GetUserByIdAsync(int id);
        Task<UserDto> AddUserAsync(AddUserDto input);
    }
}
