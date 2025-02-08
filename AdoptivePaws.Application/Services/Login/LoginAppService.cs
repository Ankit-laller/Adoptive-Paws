using AdoptivePaws.Core.Common;
using AdoptivePaws.Core.Dtos;
using AdoptivePaws.Core.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

using System.Text;
namespace AdoptivePaws.Core.Interfaces.User
{
    public class LoginAppService(IUserRepository userRepository,IConfiguration _configuration,ICommonAppService commonAppService) : ILoginAppService
    {
        public async Task<LoginReponse> LoginAsync(LoginDto input)
        {
            UserEntity user = null;
            if (input.Email != "")
            {
                user = (await userRepository.GetAllUsersAsync()).Where(x => x.Email.Equals(input.Email, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
                if(user is null)
                {
                    return null;
                }
            }
            var passwordHash = user!=null ? commonAppService.ComputeHash(input.Password, user.PasswordSalt, 3):Task.FromResult("");
            if(passwordHash.Result.ToString() != user.PasswordHash)
            {
                return null;
            }
            /*var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetConnectionString("UniqueKey")); // Use the same secret key as configured
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, input.EmailAddress)
                }),
                Expires = DateTime.UtcNow.AddHours(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);*/
            var token = GenerateJwtToken(user);
            return new LoginReponse
            {
                Name = user.Name,
                City = user.City,
                EmailAddress = user.Email,
                PhoneNo = user.PhoneNo,
                token = token,
            };

        }

        public string GenerateJwtToken(UserEntity user)
        {
            try
            {
                var _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
                // Retrieve the JWT secret from environment variables and encode it
                var key = Encoding.ASCII.GetBytes(_configuration.GetConnectionString("JWT_SECRET")!);

                //predefined claims
                // Create claims for user identity and role
                /*var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Name),
                
                //new Claim(ClaimTypes.Role, role)
                new Claim(ClaimTypes.Email,user.Email),
            };
                var identity = new ClaimsIdentity(claims);*/


                //custom claims
                // Create an identity from the claims
                var identity = new ClaimsIdentity();
                identity.AddClaim(new Claim("UserName", user.Name));
                identity.AddClaim(new Claim("UserEmail", user.Email));
                identity.AddClaim(new Claim("UserId", user.SNo.ToString()));

                // Describe the token settings
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Issuer = _configuration.GetConnectionString("JWT_ISSUER"),
                    Audience = _configuration.GetConnectionString("JWT_AUDIENCE"),
                    Subject = identity,
                    Expires = DateTime.UtcNow.AddMinutes(30),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                // Create a JWT security token
                var token = _jwtSecurityTokenHandler.CreateJwtSecurityToken(tokenDescriptor);

                // Write the token as a string and return it
                return _jwtSecurityTokenHandler.WriteToken(token);
            }catch(Exception ex)
            {
                throw ex;
            }
        }

       
    }
}
