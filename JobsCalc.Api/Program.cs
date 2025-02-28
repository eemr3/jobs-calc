using System.Text;
using JobsCalc.Api.Filters;
using JobsCalc.Application.UseCases.Auth;
using JobsCalc.Application.UseCases.User;
using JobsCalc.Application.Validators;
using JobsCalc.Common.Services.Authentication;
using JobsCalc.Domain.Interfaces;
using JobsCalc.Infrastructure.Persistence;
using JobsCalc.Infrastructure.Repositories;
using JobsCalc.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

var secretKey = builder.Configuration["Jwt:Secret"];
if (string.IsNullOrEmpty(secretKey)) throw new ArgumentNullException("JWT secret key is not configured.");

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowMyOrigin", p =>
    {
        p.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddControllers();
builder.Services.AddMvc(options => options.Filters.Add(new ExceptionFilter()));

builder.Services.AddDbContextFactory<ApiDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<RegisterUserValidator>();
builder.Services.AddScoped<IRegisterUseCase, RegisterUseCase>();
builder.Services.AddSingleton<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<IAuthUseCase, AuthUseCase>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddSingleton<IJwtTokenGenerator>(new JwtTokenGenerator(secretKey));
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
            ValidAudience = builder.Configuration["Jwt:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
        };
    });
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowMyOrigin");

app.UseAuthorization();
app.MapControllers();

app.Run();