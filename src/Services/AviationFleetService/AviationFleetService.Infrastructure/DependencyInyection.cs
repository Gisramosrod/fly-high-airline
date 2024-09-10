using AviationFleetService.Domain.Repositories;
using AviationFleetService.Infrastructure.Common;
using AviationFleetService.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using MassTransit;

namespace AviationFleetService.Infrastructure
{
    public static class DependencyInyection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Database");
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException("The connection string is null");
            }

            services.AddDbContext<AppDbContext>(
                (sp, options) =>
                {
                    options.UseSqlServer(connectionString);
                });

            services.AddMassTransit(busConfigurator =>
            {
                busConfigurator.SetKebabCaseEndpointNameFormatter();

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
            services.AddScoped<IPlaneRepository, PlaneRepository>();
            services.AddScoped<ISeatRepository, SeatRepository>();
            services.AddScoped<IPlaneServiceRepository, PlaneServiceRepository>();

            return services;
        }
    }
}
