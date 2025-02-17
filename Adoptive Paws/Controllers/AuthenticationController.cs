using AdoptivePaws.Application.Prodcut.Queries.Login;
using AdoptivePaws.Core.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Adoptive_Paws.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController(ISender sender) : ControllerBase
    {
        [HttpPost("login")]
        public async Task<IActionResult> LoginUser(LoginDto login)
        {
            var result = await sender.Send(new LoginQuery(login));
            return Ok(result);
        }
        [HttpGet("ValidateToken")]
        public async Task<IActionResult> ValidateTokenAsync(string token)
        {
            var result = await sender.Send(new ValidateTokenQuery(token));
            return Ok(result);
        }
    }
}
