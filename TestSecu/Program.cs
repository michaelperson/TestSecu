using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using SecurityTools.Models;
using System.Text;
using TestSecu.Domain.Repositories;
using TestSecu.Domain.Services;
using TestSecu.Infrastructure.Persistence;
using TestSecu.Infrastructure.Repositories;
using TestSecu.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

//Récup de la connection string
string cnstr = builder.Configuration.GetConnectionString("default")!;

//Récup des infos jwt 

builder.Services.Configure<JwtSettings>(
            builder.Configuration.GetSection("Jwt"));

//Ajouter le scheme Jwt
// J'ai besoin du nuget Microsoft.AspNetCore.Authentication.JwtBearer
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]!))
        };
    });
builder.Services.AddAuthorization();

// Add services to the container.
builder.Services.AddScoped<IAccountRepository, AccountRepository>(a=> new AccountRepository(cnstr));

builder.Services.AddScoped<IJWtService, JwtService>();

//Ok pour du test MAIS seul les repo ont besoin de dialoguer avec EF donc mon API n'en a pas besoin
//builder.Services.AddDbContext<TestSecuDatacontext>(a => new TestSecuDatacontext(cnstr));


builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
