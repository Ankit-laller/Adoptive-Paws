using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;
using Thinktecture.IdentityModel.Tokens;

namespace AdoptivePaws.Application.Middleware
{
    public class JwtMiddleware : IMiddleware
    {
        private readonly JwtSecurityTokenHandlerWrapper _jwtSecurityTokenHandler;
        private readonly IConfiguration _configuration;

        public JwtMiddleware(JwtSecurityTokenHandlerWrapper jwtSecurityTokenHandler, IConfiguration configuration)
        {
            _jwtSecurityTokenHandler = jwtSecurityTokenHandler;
            _configuration = configuration;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            // Get the token from the Authorization header
            var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (token is not null)
            {

                try
                {
                    // Verify the token using the JwtSecurityTokenHandlerWrapper
                    var claimsPrincipal = ValidateJwtToken(token);

                    // Extract the user ID from the token
                    var userId = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                    // Store the user ID in the HttpContext items for later use
                    context.Items["UserId"] = userId;

                    // You can also do the for same other key which you have in JWT token.
                }
                catch (Exception)
                {
                    // If the token is invalid, throw an exception
                   // context.Response = new UnauthorizedResult();
                }


            }
            // Continue processing the request
            await next(context);
        }
        public ClaimsPrincipal ValidateJwtToken(string token)
        {
            // Retrieve the JWT secret from environment variables and encode it
            var key = Encoding.ASCII.GetBytes(_configuration.GetConnectionString("JWT_SECRET")!);

            try
            {
                // Create a token handler and validate the token
                var tokenHandler = new JwtSecurityTokenHandler();
                var claimsPrincipal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = _configuration.GetConnectionString("JWT_ISSUER"),
                    ValidAudience = _configuration.GetConnectionString("JWT_AUDIENCE"),
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                }, out SecurityToken validatedToken);

                // Return the claims principal
                return claimsPrincipal;
            }
            catch (SecurityTokenExpiredException)
            {
                // Handle token expiration
                throw new ApplicationException("Token has expired.");
            }
        }
    }
    }
