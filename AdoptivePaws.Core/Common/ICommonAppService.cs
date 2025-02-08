using AdoptivePaws.Core.Dtos.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoptivePaws.Core.Common
{
    public interface ICommonAppService
    {
        Task<string> ComputeHash(string password, string salt, int iteration);
        Task<string> GenerateSalt();
        Task<BlobResponse> UploadFileToBlobAsync(IFormFile file, string containerName);
        string GenerateSelectQuery(string tableName,Dictionary<string,object> columns);
    }
}
