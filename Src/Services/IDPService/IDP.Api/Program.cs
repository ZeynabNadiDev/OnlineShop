using Asp.Versioning;
using IDP.Application.Handlers.Command.User;
using IDP.Domain.IRepository.Command;
using IDP.Domain.IRepository.Query;
using IDP.Infra.Context;
using IDP.Infra.Repository.Command;
using IDP.Infra.Repository.Query;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.Interface.UOW;
using Shared.Infra.Persistence.UOW;
using StackExchange.Redis;
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
            builder.Services.AddScoped<IUnitOfWork,UnitOfWork<IDPCommandDbContext>>();

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
            //builder.Services.AddCap(options =>
            //{
            //    // ✅ Database (Command side)
            //     options.UseSqlServer(
            //        builder.Configuration.GetConnectionString("CommandConnection")
            //    );

            //    // ✅ RabbitMQ Transport
            //    options.UseRabbitMQ(cfg =>
            //    {
            //        cfg.HostName = "localhost";
            //        cfg.Port = 5672;
            //        cfg.UserName = "guest";
            //        cfg.Password = "guest";
            //        cfg.VirtualHost = "/";
            //    });

            //    // ✅ CAP Dashboard
            //    options.UseDashboard(dashboard =>
            //    {
            //        dashboard.PathMatch = "/cap";
            //    });

            //    // ✅ Retry policy
            //    options.FailedRetryCount = 10; //count
            //    options.FailedRetryInterval = 5; //secend
            //});

            builder.Services.AddMassTransit(busConfig =>
            {
                busConfig.AddEntityFrameworkOutbox<IDPCommandDbContext>(o =>
                {
                    o.QueryDelay = TimeSpan.FromSeconds(30);
                    o.UseSqlServer().UseBusOutbox();
                });
                busConfig.SetKebabCaseEndpointNameFormatter();
                busConfig.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(new Uri(builder.Configuration.GetValue<string>("Rabbit:Host")), h =>
                    {
                        h.Username(builder.Configuration.GetValue<string>("Rabbit:UserName"));
                        h.Password(builder.Configuration.GetValue<string>("Rabbit:Password"));

                    });
                    cfg.UseMessageRetry(r => r.Exponential(10, TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(30),TimeSpan.FromSeconds(10)));
                    cfg.ConfigureEndpoints(context);
                });
            });

            Auth.Extensions.AddJwt(builder.Services, builder.Configuration);

            builder.Services.AddDbContext<IDPCommandDbContext>(options =>
     options.UseSqlServer(builder.Configuration.GetConnectionString("CommandConnection")));

            builder.Services.AddDbContext<IDPQueryDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("QueryConnection")));
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
