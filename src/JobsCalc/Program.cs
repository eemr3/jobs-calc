using JobsCalc.Api.Application.Services.AuthService;
using JobsCalc.Api.Application.Services.UserService;
using JobsCalc.Api.Http.Filters;
using JobsCalc.Api.Infra.Database.EntityFramework;
using JobsCalc.Api.Infra.Database.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options => options.Filters.Add<CustomExceptionFilter>());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration["ConnectionStrings:DefaultConnection"];
builder.Services.AddDbContext<IAppDbContext, AppDbContext>(options => options.UseNpgsql(connectionString));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthSevice, AuthService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
