using AdoptivePaws.Core.Common;
using AdoptivePaws.Core.Dtos.User;
using AdoptivePaws.Core.Entities;
using AdoptivePaws.Core.Interfaces.User;
using AutoMapper;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AdoptivePaws.Application.Services.User
{
    public class UserAppService : IUserAppService
    {
        private readonly IUserRepository _userRepo;
        private readonly ICommonAppService _commonAppService;
        private readonly IMapper _mapper;
        public UserAppService(IUserRepository userRepo,ICommonAppService commonAppService,IMapper mappper)
        {
            _userRepo= userRepo;
            _commonAppService= commonAppService;
            _mapper= mappper;
        }
        public async Task<UserDto> AddUserAsync(AddUserDto input)
        {
            var passwordSalt= _commonAppService.GenerateSalt(); ;
            var passwordHash = _commonAppService.ComputeHash(input.Password, passwordSalt.Result.ToString(), 3);
            AddUserDataDto userData = new AddUserDataDto()
            {
                Name = input.Name,
                PhoneNo = input.PhoneNo,
                Password =input.Password,
                Email = input.Email,
                City = input.City,
                PasswordHash = passwordHash.Result.ToString(),
                PasswordSalt = passwordSalt.Result.ToString()
            };
            var addedUser = await _userRepo.AddUserAsync(userData);
            return _mapper.Map<UserDto>(addedUser);
        }

        public async Task<UserDto> GetUserByIdAsync(int id)
        {
           
            return await _userRepo.GetUserByIdAsync(id);
        }
        

        

    }
}
