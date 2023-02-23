using BLL.Managers;
using BLL.Managers.Contracts;
using BLL.Services;
using BLL.Services.Contracts;
using BLL.Validators;
using BLL.Validators.Contracts;
using DAL.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace PL.Extensions
{
    public static class ServicesExtensions
    {
        public static void AddAuth(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = configuration["Auth:Issuer"],
                        RequireAudience = true,
                        ValidateAudience = true,
                        ValidAudience = configuration["Auth:Audience"],
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(configuration["Auth:Secret"])),
                        RequireExpirationTime = true
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            var accessToken = context.Request.Query["token"];

                            if (!string.IsNullOrWhiteSpace(accessToken) &&
                                context.Request.Path.StartsWithSegments("/tasks"))
                            {
                                context.Token = accessToken;
                            }

                            return Task.CompletedTask;
                        }
                    };
                });

            services.AddAuthorization(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser()
                    .Build();
            });
        }

        public static void AddServices(this IServiceCollection services)
        {
            //TokenManager
            services.AddScoped<ITokenManager, TokenManager>();

            //Validators
            services.AddScoped<IPasswordValidator, PasswordValidator>();
            services.AddScoped<IEmailValidator, EmailValidator>();

            //Repositories
            services.AddScoped<IRepository<DAL.Models.User>, Repository<DAL.Models.User>>();
            services.AddScoped<IRepository<DAL.Models.Task>, Repository<DAL.Models.Task>>();
            services.AddScoped<IRepository<DAL.Models.Comment>, Repository<DAL.Models.Comment>>();

            //Services
            services.AddScoped<IUserService, UserService>();
        }
    }
}
