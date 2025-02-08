using AdoptivePaws.Application.Prodcut.Commands.Pet;
using AdoptivePaws.Application.Prodcut.Queries.Pet;
using AdoptivePaws.Core.Dtos.Pet;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Adoptive_Paws.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PetController : ControllerBase
    {
        private readonly ISender _sender;
        public PetController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("SavePetData")]
        public async Task<ActionResult> SavePetData([FromForm] PetModelDto pet)
        {
            return Ok( await _sender.Send(new AddPetCommand(pet)));
        }
        [HttpGet("GetAllPets")]
        public async Task<ActionResult> GetAllPets()
        {
            return Ok(await _sender.Send(new GetAllPetsDataQuery()));
        }
        [HttpGet("GetPetDataByPetType")]
        public async Task<ActionResult> GetPetDataByPetType(string petType)
        {
            return Ok(await _sender.Send(new GetPetDataByPetTypeQuery(petType)));
        }
        [HttpGet("GetAdoptionRequests")]
        public async Task<ActionResult> GetAdoptionRequests()
        {
            var rs = await _sender.Send(new GetAllAdoptionRequestQuery());
            return rs != null ? Ok(rs) : NotFound();
        }

        [HttpGet("RejectAdoptionRequest")]
        public async Task<ActionResult> RejectAdoptionRequest(string petId)
        {
            var rs = await _sender.Send(new RejectAdoptionRequestCommand(petId));
            return rs != null ? Ok(rs) : NotFound();
        }
        [HttpPost("SendAdoptionRequest")]
        public async Task<ActionResult> SendAdoptionRequest(AdoptionRequestDto input)
        {
            var rs = await _sender.Send(new SendAdoptionRequestCommand(input));
            return rs != null ? Ok(rs) : NotFound();
        }
        [HttpGet("AcceptAdoptionRequest")]
        public async Task<ActionResult> AcceptAdoptionRequest(string petId)
        {
            var rs = await _sender.Send(new AcceptAdoptionRequestCommand(petId));
            return rs != null ? Ok(rs) : NotFound();
        }
    }
}
