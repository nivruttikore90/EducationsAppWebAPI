using BLL.Interface;
using BLL.Services;
using EducationsAppWebAPI.Filters;

namespace EducationsAppWebAPI
{
    public static class DependencyRegister
    {
        // Extension method to register services in IServiceCollection
        public static void RegisterServices(this IServiceCollection services)
        {
            // Add your service and repository registrations here(Register application services)
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ICacheProvider, CacheProvider>();
            services.AddScoped<IInstitute, Institute>();
            services.AddScoped<IStudent, Student>();
            services.AddTransient<ITransientService, OperationService>();
            services.AddScoped<IScopedService, OperationService>();
            services.AddSingleton<ISingletonService, OperationService>();

            services.AddScoped<CustomAuthorizationFilter>();
            services.AddScoped<CacheResourceFilter>();
            services.AddScoped<LogActionFilter>();
            services.AddSingleton<GlobalExceptionFilter>();
            services.AddScoped<ModifyResultFilter>();
        }
    }
}
