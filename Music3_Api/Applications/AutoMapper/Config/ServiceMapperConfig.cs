

using Microsoft.Extensions.DependencyInjection;

namespace Music3_Api.AutoMapper.Config
{
    public static class ServiceMapper
    {
        public static void AddMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
        }
    }
}
