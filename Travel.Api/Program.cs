using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using NLog;
using NLog.Web;
using System.Reflection;
using Travel.Api.Configurations;
using Travel.Api.Middlewares;
using Travel.Application.Infra_Contracts;
using Travel.Domain.Tools.Logging;
using Travel.Infrastructure.Persistence;
using Travel.Infrastructure.Persistence.Repositories;

namespace Travel.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
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

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}