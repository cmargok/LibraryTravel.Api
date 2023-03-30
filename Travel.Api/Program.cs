using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NLog;
using NLog.Web;
using System.Reflection;
using System.Text;
using Travel.Api.Configurations;
using Travel.Api.Middlewares;
using Travel.Application.Infra_Contracts;
using Travel.Domain.Tools.Logging;
using Travel.Infrastructure.Persistence;
using Travel.Infrastructure.Persistence.Repositories;


var logger = LogManager.GetLogger("FileConsoleLogger");
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.RegisterRepositories();
builder.Services.RegisterServices();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("TravelDB")));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Travel library Api",
        Version = "v1",
        Description = " Api para la visualizacion de los libros, autores y editoriales suscriptas a Travel",
        License = new OpenApiLicense
        {
            Name = "Open license",
        }
    });

    options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        Description = "@JWT Authorization header using the Bearer Scheme." +
                        "Ingrese 'Bearer [space] and then your token in the text input below." +
                        "Ejemplo = 'Bearer 123456abcdefg'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = JwtBearerDefaults.AuthenticationScheme,

                    },
                    Name = JwtBearerDefaults.AuthenticationScheme,
                    In = ParameterLocation.Header,
                    Scheme = "0auth2"
                },
                new List<string>()

            }
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory,xmlFile);
    options.IncludeXmlComments(xmlPath);
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyHeader()
        .AllowAnyMethod()
        .AllowAnyOrigin());
});

//===================Logging ================
builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Information);
builder.Host.UseNLog();
builder.Services.Configure<LoggingSettings>(builder.Configuration.GetSection("LogSettings"));
builder.Services.AddSingleton<IApiLogger, LoggerManager>();




//===================== JWT =========================

builder.Services.AddAuthentication(
    options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        // options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(JWToptions =>
    {
        JWToptions.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero,
            ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
            ValidAudience = builder.Configuration["JwtSettings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"])),
        };
    }
);

var app = builder.Build();





// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI( sw =>
    {
        sw.DocumentTitle = "Travel library Api";
    });
}

//==================== MiddleWares ===============================
app.UseMiddleware<ExceptionHandlerMiddleware>();
app.UseMiddleware<OperationCanceledMiddleware>();

app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseMiddleware<RequestMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
