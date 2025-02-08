using AdoptivePaws.Core.Common;
using Dapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace AdoptivePaws.Application.Middleware
{
    public class ApiResponseMiddleware
    {
        private readonly RequestDelegate _next;

        public ApiResponseMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Store the original response body stream
            var originalResponseBodyStream = context.Response.Body;

            using (var newResponseBodyStream = new MemoryStream())
            {
                // Set the response body to the new memory stream
                context.Response.Body = newResponseBodyStream;

                // Call the next middleware (this will invoke the controller action)
                await _next(context);

                // Read the response body after the controller action has executed
                var responseBody = await ReadResponseBodyAsync(newResponseBodyStream);

                // Modify the response body by wrapping it into a new structure
                var modifiedResponse = await CreateApiResponseAsync(responseBody,context);

                // Restore the original response body stream
                context.Response.Body = originalResponseBodyStream;
                context.Response.ContentType = "application/json"; // Ensure Content-Type is correct

                // Convert the modified response to bytes and write to the original response stream
                var modifiedBytes = Encoding.UTF8.GetBytes(modifiedResponse);
                try
                {
                    if(responseBody.Length==0)
                        context.Response.StatusCode = 200;
                    await context.Response.Body.WriteAsync(modifiedBytes, 0, modifiedBytes.Length);
                }
                catch (Exception ex) { }
                return;
            }
        }
        private async Task<string> ReadResponseBodyAsync(MemoryStream memoryStream)
        {
            // Reset the memory stream position to the beginning before reading
            memoryStream.Seek(0, SeekOrigin.Begin);
            using (var reader = new StreamReader(memoryStream))
            {
                return await reader.ReadToEndAsync();
            }
        }

        private async Task<string> CreateApiResponseAsync(string responseBody, HttpContext context)
        {
            // Try parsing as a collection (array or list)
            try
            {
                // Attempt to parse as an array
                var parsedArray = JsonSerializer.Deserialize<object[]>(responseBody);
                var apiResponse = new
                {
                    success = true,
                    message = "Request processed successfully",
                    result = parsedArray
                };
                return JsonSerializer.Serialize(apiResponse);
            }
            catch (JsonException)
            {
                // If deserialization to array fails, handle as a fallback (e.g., a generic object or primitive)
                try
                {
                    var parsedObject = JsonSerializer.Deserialize<object>(responseBody);
                    var apiResponse = new
                    {
                        success = true,
                        message = "Request processed successfully",
                        result = parsedObject
                    };
                    return JsonSerializer.Serialize(apiResponse);
                }
                catch (JsonException)
                {
                    //try
                    //{
                        //var parsedObject = JsonSerializer.Deserialize<string>(responseBody);
                        var apiResponse = new
                        {
                            success = true,
                            message = "Request processed successfully",
                            result = responseBody
                        };
                        return JsonSerializer.Serialize(apiResponse);
                    //}
                    //catch (JsonException)
                    //{
                    /*    try
                        {
                            var parsedObject = JsonSerializer.Deserialize<bool>(responseBody);
                            var apiResponse = new
                            {
                                success = true,
                                message = "Request processed successfully",
                                result = parsedObject
                            };
                            return JsonSerializer.Serialize(apiResponse);
                        }catch(Exception ex)
                        {
                            DynamicParameters param = new DynamicParameters();
                            param.Add("Method", (context.Request.Path).ToString());
                            param.Add("ErrorMessage", ex.Message.ToString());
                            param.Add("ResponseJson", ex.StackTrace.ToString());
                            param.Add("ClientIpAddress", context.Connection.RemoteIpAddress.ToString());
                            //var customHeader = context.Request.Headers["x-custom-header"];



                            var query = @"insert into ApiAuditLogs ([Method],[ErrorMessage],[ResponseJson],[ClientIpAddress])
                            values(@Method,@ErrorMessage,@ResponseJson,@ClientIpAddress)";
                            await DapperSqlHelper.ExecuteSqlAsync(query, param);
                            // If still failing, handle raw response as the result
                            var apiResponse = new
                            {
                                success = false,
                                message = "Failed to process response",
                                result = responseBody
                            };
                            return JsonSerializer.Serialize(apiResponse);
                        }*/
                    //}
                    
                }
            }
        }

    }
}
