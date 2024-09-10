using FlightService.Application.Airports.Consumers;
using FlightService.Domain.Repositories;
using FlightService.Infrastructure.Common;
using FlightService.Infrastructure.Interceptors;
using FlightService.Infrastructure.Repositories;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace FlightService.Infrastructure
{
    public static class DependencyInyection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Database");
            if (connectionString.IsNullOrEmpty())
            {
                throw new ArgumentNullException("The connection string is null");
            }

            services.AddSingleton<UpdateAuditableEntitiesInterceptor>();

            services.AddDbContext<AppDbContext>(
                (sp, options) =>
                {
                    var auditableInterceptor = sp.GetService<UpdateAuditableEntitiesInterceptor>()!;

                    options.UseSqlServer(connectionString).AddInterceptors(auditableInterceptor);
                });

            services.AddMassTransit(busConfigurator =>
            {
                busConfigurator.SetKebabCaseEndpointNameFormatter();

                busConfigurator.AddConsumer<AirportCreatedEventConsumer>();
                busConfigurator.AddConsumer<AirportDeletedEventConsumer>();
                busConfigurator.AddConsumer<AirportUpdatedEventConsumer>();

                busConfigurator.UsingRabbitMq((context, configurator) =>
                {
                    configurator.Host(new Uri(configuration["MessageBroker:Host"]!), h =>
                    {
                        h.Username(configuration["MessageBroker:Username"]);
                        h.Password(configuration["MessageBroker:Password"]);
                    });

                    configurator.ConfigureEndpoints(context);
                });
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAirportRepository, AirportRepository>();
            services.AddScoped<IFlightDetailRepository, FlightDetailRepository>();
            services.AddScoped<IFlightRepository, FlightRepository>();
            services.AddScoped<IPlaneRepository, PlaneRepository>();

            return services;
        }
    }
}
