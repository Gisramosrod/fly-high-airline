using FlightService.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FlightService.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
            });

            services.AddScoped<IFlightNumberService, FlightNumberService>();

            return services;
        }
    }
}
