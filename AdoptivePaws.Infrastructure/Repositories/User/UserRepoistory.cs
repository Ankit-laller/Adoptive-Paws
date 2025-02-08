using AdoptivePaws.Infrastructure.Data;
using AdoptivePaws.Core.Entities;
using Microsoft.EntityFrameworkCore;
using AdoptivePaws.Core.Common;
using AdoptivePaws.Core.Interfaces.User;
using AdoptivePaws.Core.Dtos.User;
using System.Collections.Generic;

namespace AdoptivePaws.Infrastructure.Repositories.User
{
    public class UserRepoistory(AppDbContext dbContext) : IUserRepository
    {
        public async Task<UserEntity> AddUserAsync(AddUserDataDto input)
        {
            if (input is not null)
            {
                var check = input.Email != null ? await dbContext.Users.FirstOrDefaultAsync(x => x.Email == input.Email) : new UserEntity();
                if (check != null) { return new UserEntity(); }
                //var passwordSalt = CommonAppService.GenerateSalt();
               // var passwordHash = CommonAppService.ComputeHash(input.Password, passwordSalt, 3);
                UserEntity user = new UserEntity();
                user.Name = input.Name;
                user.Email = input.Email;
                user.PhoneNo = input.PhoneNo;
                user.PasswordSalt = input.PasswordSalt;
                user.PasswordHash = input.PasswordHash;
                user.City = input.City;
                await dbContext.Users.AddAsync(user);
                await dbContext.SaveChangesAsync();
                return user;
            }
            return new UserEntity();
        }

        public async Task<bool> DeleteUser(int id)
        {
            var user = await dbContext.Users.FirstOrDefaultAsync(x => x.TenantId == id);

            if (user is not null)
            {
                dbContext.Users.Remove(user);

                return await dbContext.SaveChangesAsync() > 0;
            }

            return false;
        }

        public async Task<List<UserEntity>> GetAllUsersAsync()
        {
            var rs = await dbContext.Users.ToListAsync();
            /*List<UserDto> users = new List<UserDto>();
            foreach (var item in rs)
            {
                UserDto user = new UserDto();
                user.Id = item.SNo;
                user.Name = item.Name;
                user.Email = item.Email;
                user.PhoneNo = item.PhoneNo;
                user.City = item.City;
                users.Add(user);
            }*/
            return rs;
        }

        public async Task<UserDto> GetUserByIdAsync(int id)
        {
            var data = await dbContext.Users.FirstOrDefaultAsync(x => x.SNo == id);
            return new UserDto
            {
                Id = data.SNo,
                Name = data.Name,
                Email = data.Email,
                City = data.City,
                PhoneNo = data.PhoneNo,
            };
        }


        public async Task<UserEntity> UpdateUserData(AddUserDto input)
        {
            if (input is not null)
            {
                var user = await dbContext.Users.FirstOrDefaultAsync(x => x.TenantId == input.Id);
                if (user is not null)
                {
                    user.Name = input.Name;
                    user.Email = input.Email;
                    user.PhoneNo = input.PhoneNo;
                    // user.Password = input.Password;
                    await dbContext.SaveChangesAsync();
                    return user;
                }

            }
            return new UserEntity();
        }
    }
}
