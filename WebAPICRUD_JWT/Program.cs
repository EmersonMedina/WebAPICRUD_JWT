using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebAPICRUD_JWT.Data;
using WebAPICRUD_JWT.Data.Interfaces;
using WebAPICRUD_JWT.Mapper;
using WebAPICRUD_JWT.Services;
using WebAPICRUD_JWT.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<DataContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("MysqlLocalhost");
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

builder.Services.AddControllers();

//Agregar el repositorio
builder.Services.AddScoped<IApiRepository, ApiRepository>();

// se agrega el repositorio de authenticacion 
builder.Services.AddScoped<IAuthRepository, AuthRepository>();

//Agregar token service 
builder.Services.AddScoped<ITokenService, TokenService>();

//Agregar automapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);

// Agregar configuración para el uso de Token
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Token"])),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Agregamos la authenticacion antes de la autorización
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
