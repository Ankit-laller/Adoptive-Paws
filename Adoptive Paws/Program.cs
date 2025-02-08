using AdoptivePaws.Application.Middleware;
using AdoptivePaws.Application.Services.Notification;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace Adoptive_Paws
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // JWT Configuration
            builder.Services.AddTransient<ExceptionLogginMiddleware>();
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false; //enable for production
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration.GetConnectionString("JWT_ISSUER"),
                    ValidAudience = builder.Configuration.GetConnectionString("JWT_AUDIENCE"),
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetConnectionString("JWT_SECRET")!)),
                   // ValidateIssuer = false,
                   // ValidateAudience = false,
                    //ValidateLifetime = true,
                   // ClockSkew = TimeSpan.Zero
                };
            });
            builder.Services.AddHttpContextAccessor();

            //for future reference
            // Configure the default authorization policy
            /*  builder.Services.AddAuthorization(options =>
              {
                  options.DefaultPolicy = new AuthorizationPolicyBuilder()
                          .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                          .RequireAuthenticatedUser()
                          .Build();

              })*/
            ;


            builder.Services.AddSwaggerGen(c =>
            {
                // Add JWT Bearer authentication to Swagger
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: 'Bearer {token}'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
            });
         //   builder.Services.AddTransient<JwtMiddleware>();

            // Add the global scope for JwtSecurityTokenHandlerWrapper.
            // builder.Services.AddScoped<JwtSecurityTokenHandlerWrapper>();

            builder.Services.AddControllers();
           /* builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin", policy =>
                {
                    policy.WithOrigins("http://localhost:4200")  // Angular app URL
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials();  // Allow credentials (cookies)
                });
            });*/
            builder.Services.AddSignalR();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddAppDI(builder.Configuration);
            builder.Services.AddSwaggerGen();
          //  builder.Services.AddTransient<IConfiguration>();
           // builder.Services.AddTransient<JwtSecurityTokenHandlerWrapper>();// Register ITokenService implementation

            //builder.Services.Configure<ConnectionStringOptions>(builder.Configuration.GetSection(ConnectionStringOptions.SectionName));
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors(builder =>
            {
                builder.AllowAnyOrigin()  // This line allows requests from any origin
                       .AllowAnyHeader()
                       .AllowAnyMethod();
                //.AllowCredentials();
            });

           // app.UseCors("AllowSpecificOrigin");
            app.UseAuthentication();
            app.UseAuthorization();
             
            app.UseMiddleware<ExceptionLogginMiddleware>();
            // app.UseMiddleware<JwtMiddleware>(); // JWT Middleware configuration
           app.UseMiddleware<ApiResponseMiddleware>();
            
            app.UseHttpsRedirection();

            app.MapControllers();
            app.MapHub<NotificationHub>("notificationHub");
             //   .RequireCors("AllowSpecificOrigin");



            app.Run();
        }
    }
}
