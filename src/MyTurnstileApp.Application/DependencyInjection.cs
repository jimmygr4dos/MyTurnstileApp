using Microsoft.Extensions.DependencyInjection;
using MyTurnstileApp.Application.Interfaces;
using MyTurnstileApp.Application.Services;

namespace MyTurnstileApp.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddTransient<IUserSubmissionService, UserSubmissionService>();
            return services;
        }
    }
}
