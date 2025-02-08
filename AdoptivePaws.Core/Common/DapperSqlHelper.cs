using AdoptivePaws.Core.Options;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;


namespace AdoptivePaws.Core.Common
{
    public class DapperSqlHelper
    {
       // private static IConfiguration configuration;
       
        public  async static Task<int> ExecuteSqlAsync(string query,DynamicParameters param)
        {
           // var connectionString = configuration.GetConnectionString("DefaultConnection");
            using (var connection = new SqlConnection("Server=localhost\\SQLEXPRESS;Database=AdoptivePaws;Connection Timeout=30;Integrated Security=True;Encrypt=False;Trusted_Connection=True;"))
            {
                await connection.OpenAsync();
                return await connection.ExecuteAsync(query,param);
            }

        }
    }
}
//