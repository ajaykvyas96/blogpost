using AutoMapper;
using GnosisNet.API.Helpers;
using GnosisNet.API.Middleware;
using GnosisNet.Repository.Interface;
using GnosisNet.Repository.Service;
using GnosisNet.Service.IServices;
using GnosisNet.Service.Models;
using GnosisNet.Service.Services;

namespace GnosisNet.API.Extensions
{
    public static class ConfigApplicationServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ITokenManagerService, TokenManagerService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IBlogService, BlogService>();
            services.AddTransient<ExceptionHandlingMiddleware>();
            services.AddTransient<ResponseDto>();

            IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
            services.AddSingleton(mapper);
            services.AddAutoMapper(typeof(MappingConfig));
            services.AddHttpContextAccessor();

            return services;
        }
    }
}
