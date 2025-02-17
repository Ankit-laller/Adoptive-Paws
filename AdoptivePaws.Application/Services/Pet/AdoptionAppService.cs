using AdoptivePaws.Core.Common;
using AdoptivePaws.Core.Dtos.Pet;
using AdoptivePaws.Core.Interfaces.Pet;
using AdoptivePaws.Core.Queries;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AdoptivePaws.Application.Services.Pet
{
    public class AdoptionAppService : IAdoptionAppService
    {
        private readonly IConfiguration _configuration;
        private readonly ICommonAppService _commonAppService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AdoptionAppService(IConfiguration configuration, ICommonAppService commonAppService, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _commonAppService = commonAppService;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<bool> AcceptAdoptionRequest(string id)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var affectedRows = await connection.ExecuteAsync(AdoptionAppServiceQueries.acceptAdoptionRequestQuery,
                            new { Id = id }, transaction);
                        if (affectedRows == 1)
                        {
                            var query = "select * from adoptionrequests where requestId= @Id";
                            var data = await connection.QueryFirstOrDefaultAsync<AdoptionRequest>(query, new { Id = id }, transaction);
                            var row = await connection.ExecuteAsync(AdoptionAppServiceQueries.adoptionQuery, new { Id = data.petId }, transaction);
                            if (row == 1)
                            {
                                transaction.Commit();
                                return true;
                            }
                        }
                        transaction.Rollback();
                        return false;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }

        public async Task<string> DeleteAdoptionRequest(string id)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var affectedRows = await connection.ExecuteAsync(AdoptionAppServiceQueries.deleteAdoptionRequestQuery,
                        new { Id = id }, transaction);
                        if (affectedRows == 1)
                        {
                            transaction.Commit();
                            return "request deleted successfully";
                        }
                        transaction.Rollback();
                        return  "failed to delete request";
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        return "failed to delete request";
                    }
                }
            }
        }

        public async Task<List<AdoptionRequest>> GetAllAdoptionRequest()
        {
            var userClaims = _httpContextAccessor.HttpContext?.User?.Identity as ClaimsIdentity;
            var userId = userClaims?.Claims?.FirstOrDefault(c => c.Type == "UserId")?.Value;
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var data = await connection.QueryAsync<AdoptionRequest>(AdoptionAppServiceQueries.fetchAdoptionRequestQuery,
                        new { Id = userId });
                return data.ToList();

            }
        }


        public async Task<string> SendAdoptionRequest(AdoptionRequestDto value)
        {
            /*value.senderId = "3";
            value.senderName = "Ankit";*/
            var userClaims = _httpContextAccessor.HttpContext?.User?.Identity as ClaimsIdentity;
            var userName = userClaims?.Claims?.FirstOrDefault(c=>c.Type=="UserName")?.Value;
            var userEmail = userClaims?.Claims?.FirstOrDefault(c => c.Type == "UserEmail")?.Value;
            var userId = userClaims?.Claims?.FirstOrDefault(c => c.Type == "UserId")?.Value;
           
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var columns = new Dictionary<string, object>
                {
                    { "petId", value.petId },
                };
                var query = _commonAppService.GenerateSelectQuery("Pets", columns);
                var isvalidRequest =await connection.QueryAsync(query);
                if (isvalidRequest == null || isvalidRequest?.Count() == 0)
                {
                    return "invalid pet id";
                }
                 columns = new Dictionary<string, object>
                {
                    { "SNo", userId },//sender id
                };
                query = _commonAppService.GenerateSelectQuery("users", columns);
                isvalidRequest = await connection.QueryAsync(query);
                if (isvalidRequest == null || isvalidRequest?.Count() == 0)
                {
                    return "invalid user id";
                }

                columns = new Dictionary<string, object>
                {
                    { "SNO", value.ownerId },
                };
                query = _commonAppService.GenerateSelectQuery("users", columns);
                isvalidRequest = await connection.QueryAsync(query);
                if (isvalidRequest == null || isvalidRequest?.Count() == 0)
                {
                    return "invalid owner id";
                }

                Guid myuuid = Guid.NewGuid();
                string myuuidAsString = myuuid.ToString();
                await connection.OpenAsync();
                var data = await connection.QueryAsync<AdoptionRequest>(AdoptionAppServiceQueries.checkRequest, new
                {
                    SenderId = userId,
                    PetId = value.petId
                });
                if (data.Count() > 0)
                {
                    return "already requested";
                }
               

                using (var transaction = connection.BeginTransaction())
                {

                    try
                    {
                        var affectedRow = await connection.ExecuteAsync(AdoptionAppServiceQueries.sendAdoptionRequestQuery,
                        new
                        {
                            RequestId = myuuidAsString,
                            SenderId = userId,
                            SenderName = userName,
                            OwnerId = value.ownerId,
                            PetId = value.petId,
                            PetName = value.petName,
                        }, transaction);
                        if (affectedRow == 1)
                        {
                            transaction.Commit();
                            return "request sent";
                        }
                        transaction.Rollback();
                        return  "failed to send request";
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        return "failed to send request";
                    }

                }
            }
        }
    }
}
