using Asp.Versioning;
using IDP.Application.Handlers.Command.User;
using IDP.Domain.IRepository.Command;
using IDP.Domain.IRepository.Query;
using IDP.Infra.Context;
using IDP.Infra.Repository.Command;
using IDP.Infra.Repository.Query;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.Interface.UOW;
using Shared.Infra.Persistence.UOW;
using StackExchange.Redis;
using System;
using System.Reflection;
namespace IDP.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddMediatR(typeof(UserHandler).GetTypeInfo().Assembly);
            builder.Services.AddScoped<IOtpRedisRepository, OtpRedisRepository>();
            builder.Services.AddScoped<IUserCommandRepository, UserCommandRepository>();
            builder.Services.AddScoped<IUserQueryRepository, UserQueryRepository>();
            builder.Services.AddScoped<IOtpRedisQueryRepository, OtpRedisQueryRepository>();
            builder.Services.AddScoped<IUnitOfWork,UnitOfWork<ShopDBContext>>();

            builder.Services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1);
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ApiVersionReader = ApiVersionReader.Combine(
                    new UrlSegmentApiVersionReader(),
                    new HeaderApiVersionReader("X-Api-Version"));
            })
       .AddMvc() // This is needed for controllers
       .AddApiExplorer(options =>
       {
           options.GroupNameFormat = "'v'V";
           options.SubstituteApiVersionInUrl = true;
       });

            Auth.Extensions.AddJwt(builder.Services, builder.Configuration);

            builder.Services.AddDbContext<ShopDBContext>(options =>
            {
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection")
                );
            });
            builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
            {
                var configuration = builder.Configuration.GetConnectionString("Redis");
                return ConnectionMultiplexer.Connect(configuration);
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
