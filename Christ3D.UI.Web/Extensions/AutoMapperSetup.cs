﻿
namespace Christ3D.UI.Web.Extensions
{
    /// <summary>
    /// AutoMapper 的启动服务
    /// </summary>
    public static class AutoMapperSetup
    {
        public static void AddAutoMapperSetup(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            //添加服务
            //services.AddAutoMapper();
            //启动配置
            Christ3D.Application.AutoMapper.AutoMapperConfig.RegisterMappings();
        }
    }
}
