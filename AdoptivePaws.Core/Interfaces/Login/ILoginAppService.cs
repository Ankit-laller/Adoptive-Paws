using AdoptivePaws.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoptivePaws.Core.Interfaces.User
{
    public interface ILoginAppService
    {
        Task<LoginReponse> LoginAsync(LoginDto input);
    }
}
