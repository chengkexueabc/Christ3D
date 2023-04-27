
using Christ3D.Application.AutoMapper;

namespace Christ3D.UI.Extensions
{
    /// <summary>
    /// AutoMapper 的启动服务
    /// </summary>
    public static class AutoMapperSetup
    {
        public static void AddAutoMapperSetup(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddAutoMapper(typeof(AutoMapperConfig));
            //启动配置
            AutoMapperConfig.RegisterMappings();
        }
    }
}
