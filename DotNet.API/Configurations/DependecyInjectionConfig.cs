using DotNet.Application.Applications;
using DotNet.Application.Interfaces;
using DotNet.Domain.Core.Interfaces.Repositories;
using DotNet.Domain.Core.Interfaces.Services;
using DotNet.Domain.Core.Notification;
using DotNet.Domain.Service;
using DotNet.Instrastructure.Data.Repositories;

namespace DotNet.Presentation.Configurations
{
    public static class DependecyInjectionConfig
    {
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            services.AddScoped<IAlunoApplication, AlunoApplication>();
            services.AddScoped<IAlunoService, AlunoService>();
            services.AddScoped<IAlunoRepository, AlunoRepository>();

            services.AddScoped<ITurmaApplication, TurmaApplication>();
            services.AddScoped<ITurmaService, TurmaService>();
            services.AddScoped<ITurmaRepository, TurmaRepository>();

            services.AddSingleton<INotifier, Notifier>();
        }
    }
}