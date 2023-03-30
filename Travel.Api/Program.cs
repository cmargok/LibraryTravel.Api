using Microsoft.EntityFrameworkCore;
using Travel.Api.Configurations;
using Travel.Api.Middlewares;
using Travel.Application.Infra_Contracts;
using Travel.Infrastructure.Persistence;
using Travel.Infrastructure.Persistence.Repositories;

namespace Travel.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.RegisterRepositories();
            builder.Services.RegisterServices();
            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("TravelDB")));


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    policy => policy.AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin());
            });




            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //==================== MiddleWares ===============================
            app.UseMiddleware<ExceptionHandlerMiddleware>();
            app.UseMiddleware<OperationCanceledMiddleware>();

            app.UseCors("AllowAll");

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}