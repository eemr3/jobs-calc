using System.Text;
using JobsCalc.Api.Filters;
using JobsCalc.Application.UseCases.Auth;
using JobsCalc.Application.UseCases.Job;
using JobsCalc.Application.UseCases.Planning;
using JobsCalc.Application.UseCases.UploadFile;
using JobsCalc.Application.UseCases.User;
using JobsCalc.Application.Validators;
using JobsCalc.Common.Services.Authentication;
using JobsCalc.Domain.Interfaces.Repositories;
using JobsCalc.Domain.Interfaces.Services;
using JobsCalc.Domain.Interfaces.UseCases;
using JobsCalc.Infrastructure.Persistence;
using JobsCalc.Infrastructure.Repositories;
using JobsCalc.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

var secretKey = builder.Configuration["Jwt:Secret"];
if (string.IsNullOrEmpty(secretKey)) throw new ArgumentNullException("JWT secret key is not configured.");

var uploadDir = builder.Configuration["FileStorage:UploadDir"];
if(string.IsNullOrEmpty(uploadDir)) throw new AggregateException("Upload directory is not configured.");

var fullUploadPath = Path.Combine(Directory.GetCurrentDirectory(), uploadDir);

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
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "JobsCalc API", Version = "v1" });

    // Definição de segurança JWT
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Por favor, insira o token JWT no campo abaixo (Bearer {token})",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.OperationFilter<FileUploadOperationFilter>();
    c.OperationFilter<AuthorizeCheckOperationFilter>();
});

builder.Services.AddScoped<RegisterUserValidator>();
builder.Services.AddSingleton<IPasswordHasher, PasswordHasher>();
builder.Services.AddSingleton<IJwtTokenGenerator>(new JwtTokenGenerator(secretKey));
builder.Services.AddScoped<IAuthUseCase, AuthUseCase>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserRegisterUseCase, UserRegisterUseCase>();
builder.Services.AddScoped<IGetUserByIdUseCase, GetUserByIdUseCase>();
builder.Services.AddScoped<IUserUpdateUseCase, UserUpdateUseCase>();
builder.Services.AddScoped<IFileUploadUseCase, FileUploadUseCase>();
builder.Services.AddScoped<IFileStorageRepository, FileStorageRepository>();
builder.Services.AddScoped<IPlanningRepository, PlanningRepository>();
builder.Services.AddScoped<ICreatePlanningUseCase, CreatePlanningUseCase>();
builder.Services.AddScoped<IGetPlanningByUserUseCase, GetPlanningByUserUseCase>();
builder.Services.AddScoped<IUpdatePlanningUseCase, UpdatePlanningUseCase>();
builder.Services.AddScoped<IJobRepository, JobRepository>();
builder.Services.AddScoped<ICreateJobUseCase, CreateJobUseCase>();
builder.Services.AddScoped<IGetJobsByUserUseCase, GetJobsByUserUseCase>();
builder.Services.AddScoped<IGetJobByIdUseCase, GetJobByIdUseCase>();
builder.Services.AddScoped<IUpdateJobUseCase, UpdateJobUseCase>();
builder.Services.AddScoped<IDeleteJobUseCase, DeleteJobUseCase>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
        };
    });
var app = builder.Build();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(fullUploadPath),
    RequestPath = "/upload/avatar"
});
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