using System.Security.Claims;
using System.Text;
using Auth.Api.Common;
using Auth.Core.Interfaces;
using Auth.Core.Services;
using Auth.Infrastructure.Data;
using Auth.Infrastructure.Repositories;
using Auth.Infrastructure.Security;
using DotNetEnv;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Auth.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Env.Load();

            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer",
                    new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                    {
                        Name = "Authorization",
                        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
                        Scheme = "bearer",
                        BearerFormat = "JWT",
                        In = Microsoft.OpenApi.Models.ParameterLocation.Header
                    });

                options.AddSecurityRequirement(
                    new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
                    {
                        {
                            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                            {
                                Reference =
                                    new Microsoft.OpenApi.Models.OpenApiReference
                                    {
                                        Type =
                                            Microsoft.OpenApi.Models
                                                .ReferenceType
                                                .SecurityScheme,
                                        Id = "Bearer"
                                    }
                            },
                            Array.Empty<string>()
                        }
                    });
            });

            builder.Services.AddDbContext<AuthDbContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection")));

            var jwtOptions = LoadJwtFromEnv();

            builder.Services.Configure<JwtOptions>(options =>
            {
                options.Issuer = jwtOptions.Issuer;
                options.Audience = jwtOptions.Audience;
                options.Key = jwtOptions.Key;
                options.AccessTokenMinutes =
                    jwtOptions.AccessTokenMinutes > 0
                        ? jwtOptions.AccessTokenMinutes
                        : 120;
            });

            builder.Services.AddAuthorization();

            builder.Services.AddScoped<AuthService>();
            builder.Services.AddScoped<ProductService>();

            builder.Services.AddScoped<IUserRepository, UsersRepository>();
            builder.Services.AddScoped<ICategoriesRepository, CategoriesRepository>();
            builder.Services.AddScoped<IProductsRepository, ProductsRepository>();

            builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
            builder.Services.AddSingleton<ITokenService, JwtTokenService>();

            var clientUrl =
                Environment.GetEnvironmentVariable("CLIENT_URL")
                ?? builder.Configuration["ClientUrl"];

            if (string.IsNullOrWhiteSpace(clientUrl))
            {
                throw new InvalidOperationException("Missing CLIENT_URL.");
            }

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(AppConstants.CorsPolicy, policy =>
                {
                    policy.WithOrigins(clientUrl)
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials();
                });
            });

            builder.Services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters =
                        new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = jwtOptions.Issuer,
                            ValidAudience = jwtOptions.Audience,
                            IssuerSigningKey =
                                new SymmetricSecurityKey(
                                    Encoding.UTF8.GetBytes(jwtOptions.Key)),
                            RoleClaimType = ClaimTypes.Role
                        };

                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            var token =
                                context.Request.Cookies[
                                    AppConstants.AccessTokenCookie];

                            if (!string.IsNullOrEmpty(token))
                            {
                                context.Token = token;
                            }

                            return Task.CompletedTask;
                        }
                    };
                });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors(AppConstants.CorsPolicy);

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
            app.Run();
        }

        private static JwtOptions LoadJwtFromEnv()
        {
            var issuer = Environment.GetEnvironmentVariable("JWT_ISSUER");
            var audience = Environment.GetEnvironmentVariable("JWT_AUDIENCE");
            var key = Environment.GetEnvironmentVariable("JWT_KEY");
            var minutesRaw =
                Environment.GetEnvironmentVariable(
                    "JWT_ACCESS_TOKEN_MINUTES");

            if (string.IsNullOrWhiteSpace(issuer)
                || string.IsNullOrWhiteSpace(audience)
                || string.IsNullOrWhiteSpace(key))
            {
                throw new InvalidOperationException(
                    "JWT environment variables missing.");
            }

            int minutes = 120;

            if (int.TryParse(minutesRaw, out var parsed))
            {
                minutes = parsed;
            }

            return new JwtOptions
            {
                Issuer = issuer,
                Audience = audience,
                Key = key,
                AccessTokenMinutes = minutes
            };
        }
    }
}
