using Christ3D.Application.Interfaces;
using Christ3D.Application.Services;
using Christ3D.Domai.Interfaces;
using Christ3D.Infrastruct.Data.Context;
using Christ3D.Infrastruct.Data.Repository;

namespace Christ3D.UI.Web.Extensions
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // 注入 Application 应用层
            services.AddScoped<IStudentAppService, StudentAppService>();

            // 注入 Infra - Data 基础设施数据层
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<StudyContext>();//上下文​
        }
    }
}
