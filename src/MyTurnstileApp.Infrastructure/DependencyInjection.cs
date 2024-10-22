using Microsoft.Extensions.DependencyInjection;
using MyTurnstileApp.Application.Interfaces;
using MyTurnstileApp.Infrastructure.Services;

namespace MyTurnstileApp.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddHttpClient<ITurnstileService, TurnstileService>();
            return services;
        }
    }
}
