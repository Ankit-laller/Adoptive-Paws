using AdoptivePaws.Core.Common;
using Azure.Core;
using Dapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace AdoptivePaws.Application.Middleware
{
    public class ExceptionLogginMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("Method", (context.Request.Path).ToString());
                param.Add("ErrorMessage", ex.Message.ToString());
                param.Add("ResponseJson", ex.StackTrace.ToString());
                param.Add("ClientIpAddress", context.Connection.RemoteIpAddress.ToString());
                //var customHeader = context.Request.Headers["x-custom-header"];
                


                var query = @"insert into ApiAuditLogs ([Method],[ErrorMessage],[ResponseJson],[ClientIpAddress])
                            values(@Method,@ErrorMessage,@ResponseJson,@ClientIpAddress)";
                
                  //  var bodyText = (await context.Request.ReadFormAsync()).ToString();

                    // Read the request body as a string (serialized format, e.g., JSON)
                   // param.Add("RequestJson", bodyText);
                    await DapperSqlHelper.ExecuteSqlAsync(query, param);
                context.Response.StatusCode = 500; // Internal Server Error
                context.Response.ContentType = "application/json";

                var response = new
                {
                    success = false,
                    message = "Something went wrong"
                };

                // Return the response as JSON
                var jsonResponse = JsonSerializer.Serialize(response);
                await context.Response.WriteAsync(jsonResponse);                //handle exception
            }
        }
    }
}
