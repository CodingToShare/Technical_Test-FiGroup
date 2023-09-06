using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;
using webapi.Data;
using webapi.Interfaces;
using webapi.Repositories;
using webapi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Obtener la cadena de conexión desde la configuración de la aplicación  
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
?? throw new InvalidOperationException("Cadena de Conexion 'DefaultConnection' no encontrada.");

// Agregar el contexto de base de datos al contenedor de dependencias  
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.User.RequireUniqueEmail = false;
})
.AddEntityFrameworkStores<DataContext>()
.AddDefaultTokenProviders();

var jwtSettings = builder.Configuration.GetSection("Jwt").Get<JwtSettings>()
?? throw new InvalidOperationException("Json 'Jwt' no encontrado.");

builder.Services.AddSingleton<IJwtSettings>(jwtSettings);

builder.Services.AddTransient<IAuthService, AuthService>();

// Agregar la implementación del repositorio de tareas al contenedor de dependencias  
builder.Services.AddTransient<ITaskRepository, TaskRepository>();

// Agregar la implementación del repositorio de los estados de las tareas al contenedor de dependencias  
builder.Services.AddTransient<ITaskStatusRepository, TaskStatusRepository>();

builder.Services.AddAuthorization()
.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})

.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = jwtSettings.Issuer,
        ValidAudience = jwtSettings.Issuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)),
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddCors(options => options.AddPolicy("AllowWebApp",
    builder => builder.WithOrigins("http://localhost:3000")
    .AllowAnyHeader()
    .AllowAnyMethod().AllowCredentials()
    ));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowWebApp");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
