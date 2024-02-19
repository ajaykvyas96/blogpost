using GnosisNet.Repository.Interface;
using GnosisNet.Repository.Service;
using GnosisNet.Service.IServices;
using GnosisNet.Service.Services;

namespace GnosisNet.API.Extensions
{
    public static class ConfigApplicationServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAuthService, AuthService>();
            return services;
        }
    }
}
