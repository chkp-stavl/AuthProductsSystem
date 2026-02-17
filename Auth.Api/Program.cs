
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using DotNetEnv;

using Microsoft.Extensions.Options;
using Auth.Core.Interfaces;
using Auth.Core.Services;
using Microsoft.AspNetCore.Identity;
using Auth.Infrastructure.Repositories;
using Auth.Infrastructure.Data;
using Auth.Infrastructure.Repositories.Read;
using Auth.Infrastructure.Interfaces;
using Auth.Infrastructure.Security;
using Microsoft.EntityFrameworkCore;
using Auth.Api.Middleware;
using Auth.Infrastructure.Repositories.Write;
using System;
using System.Security.Claims;


var hasher = new PasswordHasher();

Console.WriteLine("Admin hash:");
Console.WriteLine(hasher.Hash("Admin123!"));

Console.WriteLine("Viewer hash:");
Console.WriteLine(hasher.Hash("Viewer123!"));

Env.Load();
var jwtOptions = LoadJwtFromEnv();
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Enter: Bearer {your JWT token}"
    });
    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});



builder.Services.Configure<JwtOptions>(options =>
{
    options.Issuer = jwtOptions.Issuer;
    options.Audience = jwtOptions.Audience;
    options.Key = jwtOptions.Key;

    var minutesStr = jwtOptions.AccessTokenMinutes;

    options.AccessTokenMinutes = jwtOptions.AccessTokenMinutes > 0 ? jwtOptions.AccessTokenMinutes : 120;
    if (string.IsNullOrWhiteSpace(options.Issuer) ||
        string.IsNullOrWhiteSpace(options.Audience) ||
        string.IsNullOrWhiteSpace(options.Key))
    {
        throw new Exception("JWT configuration is missing (.env)");
    }
});





builder.Services.AddAuthorization();

builder.Services.AddDbContext<AuthDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<IUserRepository, UsersRepository>();
builder.Services.AddScoped<IReadRepository,ReadRepository>();
builder.Services.AddScoped<IWriteRepository, UserWriteRepository>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddSingleton<ITokenService, JwtTokenService>();
builder.Services.AddScoped<ICategoriesRepository, CategoriesRepository>();
builder.Services.AddScoped<IProductsRepository, Auth.Infrastructure.Repositories.ProductsRepository>();

var corsPolicy = "AllowAngular";

builder.Services.AddCors(options =>
{
    options.AddPolicy(corsPolicy, policy =>
    {
        policy
            .WithOrigins("http://localhost:4200")   // Angular
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();

    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer("Bearer", options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtOptions.Issuer,
        ValidAudience = jwtOptions.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwtOptions.Key)),
        ClockSkew = TimeSpan.Zero,
        RoleClaimType = ClaimTypes.Role

    };

    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            var token = context.Request.Cookies["access_token"];
            if (!string.IsNullOrEmpty(token))
                context.Token = token;

            return Task.CompletedTask;
        }
    };
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors(corsPolicy);
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
/*app.UseMiddleware<CsrfMiddleware>();*/
app.MapControllers();



app.Run();


JwtOptions LoadJwtFromEnv()
{
    var jwt = new JwtOptions
    {
        Issuer = Environment.GetEnvironmentVariable("JWT_ISSUER") ?? "",
        Audience = Environment.GetEnvironmentVariable("JWT_AUDIENCE") ?? "",
        Key = Environment.GetEnvironmentVariable("JWT_KEY") ?? "",
        AccessTokenMinutes = int.TryParse(
            Environment.GetEnvironmentVariable("JWT_ACCESS_TOKEN_MINUTES"),
            out var minutes) ? minutes : 120
    };

    if (string.IsNullOrWhiteSpace(jwt.Issuer) ||
        string.IsNullOrWhiteSpace(jwt.Audience) ||
        string.IsNullOrWhiteSpace(jwt.Key))
    {
        throw new Exception("JWT configuration missing in .env");
    }

    return jwt;
}