using System.Text;
using IOPath = System.IO.Path;
using DotNetEnv;
using JobsCalc.Api.Application.Services.AuthService;
using JobsCalc.Api.Application.Services.JobService;
using JobsCalc.Api.Application.Services.PlanningService;
using JobsCalc.Api.Application.Services.UserService;
using JobsCalc.Api.Http.Filters;
using JobsCalc.Api.Http.GraphQL.Queries;
using JobsCalc.Api.Http.Middleware;
using JobsCalc.Api.Infra.Database.EntityFramework;
using JobsCalc.Api.Infra.Database.Repositories;
using JobsCalc.Api.Infra.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using JobsCalc.Api.Http.GraphQL.Mutations;

var builder = WebApplication.CreateBuilder(args);

// Carregar variáveis do .env, se o arquivo existir
Env.Load(IOPath.Combine(Directory.GetCurrentDirectory(), ".env"));

var avatarPath = IOPath.Combine(Directory.GetCurrentDirectory(), "upload", "avatar");
if (!Directory.Exists(avatarPath))
{
    Directory.CreateDirectory(avatarPath);
}

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowMyOrigin", p =>
    {
        p.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// Add services to the container.

builder.Services.AddControllers(options => options.Filters.Add<CustomExceptionFilter>());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

    c.OperationFilter<AuthorizeCheckOperationFilter>();
    // c.OperationFilter<AddCustomErrorsOperationFilter>();

});

// Configurar a conexão com o banco de dados
var connectionString = Environment.GetEnvironmentVariable("STRING_CONNECTION");

if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("Connection string is not configured.");
}
builder.Services.AddDbContext<IAppDbContext, AppDbContext>(options => options.UseNpgsql(connectionString));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IPlanningRepository, PlanningRepository>();
builder.Services.AddScoped<IPlanningService, PlanningService>();
builder.Services.AddScoped<IJobRepository, JobRepository>();
builder.Services.AddScoped<IJobService, JobService>();
builder.Services.AddScoped<IUploadService, UploadService>();

// Configurar JWT
var secretKey = Environment.GetEnvironmentVariable("JWT_SECRET_KEY");
if (string.IsNullOrEmpty(secretKey))
{
    throw new InvalidOperationException("SecretKey is not configured.");
}

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidateIssuer = false,
        ValidateAudience = false,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey)),
        ClockSkew = TimeSpan.Zero
    };
});


builder.Services.AddGraphQLServer().AddAuthorization()
       .AddQueryType<Query>()
       .AddMutationType<Mutation>()
       .AddType<UploadType>();


var app = builder.Build();

app.UseMiddleware<UnauthorizedResponseMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        IOPath.Combine(Directory.GetCurrentDirectory(), "upload", "avatar")),
    RequestPath = "/upload/avatar"
});

app.UseHttpsRedirection();
app.UseCors("AllowMyOrigin");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapGraphQL();

app.Run();
