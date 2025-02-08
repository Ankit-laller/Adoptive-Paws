using AdoptivePaws.Core.Dtos.Common;
using AdoptivePaws.Core.Options;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AdoptivePaws.Core.Common
{
    public class CommonAppService:ICommonAppService
    {



        /*public static string GenerateSalt()
        {
            using var rng = RandomNumberGenerator.Create();
            var byteSalt = new byte[16];
            rng.GetBytes(byteSalt);
            var salt = Convert.ToBase64String(byteSalt);
            return salt;
        }*/
        private readonly IConfiguration _configuration;
        public CommonAppService(IConfiguration conf)
        {
            _configuration = conf;
        }
        public async Task<BlobResponse> UploadFileToBlobAsync(IFormFile file, string containerName)
        {
            var fileName = Path.GetFileNameWithoutExtension(file.FileName)
                    + "_"
                    + DateTime.Now.ToString("yyyyMMdd_HHmmss")
                    + Path.GetExtension(file.FileName);

            var blobServiceClient = new BlobServiceClient(_configuration.GetConnectionString("AzureBlobStorage"));
            var blobContainerClient = blobServiceClient.GetBlobContainerClient(containerName);

            await blobContainerClient.CreateIfNotExistsAsync();

            var blobClient = blobContainerClient.GetBlobClient(fileName);

            using (var stream = file.OpenReadStream())
            {
                await blobClient.UploadAsync(stream);
            }

            return new BlobResponse
            {
                ImageName = fileName,
                ImageUrl = blobClient.Uri.ToString()
            };
        }

        public Task<string> ComputeHash(string password, string salt, int iteration)
        {
            if (iteration <= 0)
            {
                return Task.FromResult(password);
            }
            var hash = "";
            while (iteration !=1)
            {
                using var sha256 = SHA256.Create();
                var passwordSaltPepper = $"{password}{salt}";
                var byteValue = Encoding.UTF8.GetBytes(passwordSaltPepper);
                var byteHash = sha256.ComputeHash(byteValue);
                 hash = Convert.ToBase64String(byteHash);
                iteration--;
            }

            // return await ComputeHash(hash, salt, iteration - 1);
           // var rs = ComputeHash(hash, salt, iteration - 1);
            return Task.FromResult(hash);

        }

        Task<string> ICommonAppService.GenerateSalt()
        {
            using var rng = RandomNumberGenerator.Create();
            var byteSalt = new byte[16];
            rng.GetBytes(byteSalt);
            var salt = Convert.ToBase64String(byteSalt);
            return Task.FromResult(salt);
        }

        public string GenerateSelectQuery(string tableName, Dictionary<string, object> columns)
        {
            StringBuilder query = new StringBuilder();

            // Start constructing the SELECT query
            query.Append($"SELECT * FROM {tableName} WHERE ");

            // Add each condition from the dictionary into the WHERE clause
            List<string> conditions = new List<string>();
            foreach (var column in columns)
            {
                // Handle different data types appropriately (e.g., strings should be wrapped in quotes)
                if (column.Value is string)
                {
                    conditions.Add($"{column.Key}='{column.Value}'");
                }
                else
                {
                    conditions.Add($"{column.Key}={column.Value}");
                }
            }

            // Join conditions with 'AND' and append to the query
            query.Append(string.Join(" AND ", conditions));

            // Append the ORDER BY clause
            query.Append(" ORDER BY 1 DESC");

            return query.ToString();
        }

    }
}
