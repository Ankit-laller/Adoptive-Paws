using AdoptivePaws.Application.Prodcut.Commands;
using AdoptivePaws.Application.Prodcut.Queries.Login;
using AdoptivePaws.Application.Prodcut.Queries.User;
using AdoptivePaws.Core.Dtos.User;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Adoptive_Paws.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(ISender sender) : ControllerBase
    {
        [HttpPost("SignUpUser")]
        public async Task<IActionResult> AddUser(AddUserDto input)
        {
            if (input == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await sender.Send(new AddUserCommand(input));
            return Ok(result);
        }
        [Authorize]
        [HttpGet("GetUserById")]
        public async Task<IActionResult> GetUserById(int userId)
        {
            if (userId == null)
            {
                return BadRequest();
            }
            var result = await sender.Send(new GetUserByIdQuery(userId));
            if (result != null) 
            {
                return Ok(result);
            }
            return NotFound();
            
        }
        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await sender.Send(new GetAllUsersQuery());
            return Ok(result);
        }
        

    }
}
