using AdoptivePaws.Core.Common;
using AdoptivePaws.Core.Dtos.Pet;


namespace AdoptivePaws.Core.Interfaces.Pet
{
    public interface IAdoptionAppService
    {
        Task<string> SendAdoptionRequest(AdoptionRequestDto value);
        Task<List<AdoptionRequest>> GetAllAdoptionRequest();
        Task<bool> AcceptAdoptionRequest(String id);
        Task<string> DeleteAdoptionRequest(String id);
    }
}
