using AdoptivePaws.Core.Entities;
using AdoptivePaws.Core.Dtos.User;

namespace AdoptivePaws.Core.Interfaces.User
{
    public interface IUserRepository
    {
        Task<UserDto> GetUserByIdAsync(int id);
        Task<UserEntity> AddUserAsync(AddUserDataDto input);
        Task<UserEntity> UpdateUserData(AddUserDto input);
        Task<bool> DeleteUser(int id);
        Task<List<UserEntity>> GetAllUsersAsync();
    }
}
