using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using MindCarePro.API.Middlewares;
using MindCarePro.Application.Helpers;
using MindCarePro.Application.Interfaces;
using MindCarePro.Application.Interfaces.Appointments;
using MindCarePro.Application.Interfaces.Psycholgists;
using MindCarePro.Application.Interfaces.Security;
using MindCarePro.Application.Interfaces.Shared;
using MindCarePro.Application.Interfaces.Users;
using MindCarePro.Application.Mappers.Patients;
using MindCarePro.Application.UseCases.Appointments;
using MindCarePro.Application.UseCases.Patients;
using MindCarePro.Application.UseCases.Psychologists;
using MindCarePro.Application.UseCases.Users;
using MindCarePro.Application.Validators.Appointments;
using MindCarePro.Application.Validators.Patients;
using MindCarePro.Infrastructure.Persistence;
using MindCarePro.Infrastructure.Repositories.Appointments;
using MindCarePro.Infrastructure.Repositories.Patients;
using MindCarePro.Infrastructure.Repositories.Psychologists;
using MindCarePro.Infrastructure.Repositories.Users;
using MindCarePro.Infrastructure.Security;


var builder = WebApplication.CreateBuilder(args);

// 1. Registrar o Handler
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails(); // Opcional, mas ajuda na padronização

// Services
builder.Services.AddControllers();


/*builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
}); */


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Informe o token JWT no formato: Bearer {seu_token}"
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
builder.Services.AddAutoMapper(cfg => { }, typeof(PatientProfile));

// Validators
builder.Services.AddValidatorsFromAssemblyContaining<CreatePatientRequestValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateAppointmentRequestValidator>();

// DI

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICurrentUser, MindCarePro.API.Common.CurrentUser>();

builder.Services.AddScoped<IPatientRepository, PatientRepository>();
builder.Services.AddScoped<IPsychologistRepository, PsychologistRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddScoped<IValidationService, ValidationService>();
builder.Services.AddScoped<IPasswordEncripter, PasswordEncripter>();
builder.Services.AddScoped<ITokenService, TokenService>();


// Casos de uso (UseCases) devem ser registrados para DI
builder.Services.AddScoped<CreatePatientUseCase>();
builder.Services.AddScoped<AllPatientsUseCase>();
builder.Services.AddScoped<UpdatePatientUseCase>();
builder.Services.AddScoped<DeletePatientUseCase>();

builder.Services.AddScoped<LoginUserUseCase>();

builder.Services.AddScoped<CreatePsychologistUseCase>();
builder.Services.AddScoped<AllPsychologistsUseCase>();
builder.Services.AddScoped<UpdatePsychologistUseCase>();
builder.Services.AddScoped<DeletePsychologistUseCase>();


builder.Services.AddScoped<CreateAppointmentUseCase>();
builder.Services.AddScoped<AllAppointmentsUseCase>();
builder.Services.AddScoped<UpdateAppointmentUseCase>();
builder.Services.AddScoped<DeleteAppointmentUseCase>();


var jwtSettings = builder.Configuration.GetSection("Jwt");

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.MapInboundClaims = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSettings["Key"]!)
            )
        };
    });


// CONNECTION MYSQL

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));


// ============== APP =========================

var app = builder.Build();

app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();
app.UseHttpsRedirection();
app.Run();
