

namespace Christ3D.UI.Web
{
    using Christ3D.Application.Interfaces;
    using Christ3D.Application.Services;
    using Christ3D.Domain.Interfaces;
    using Christ3D.Infrastruct.Data.Context;
    using Christ3D.Infrastruct.Data.Repository;
    using Christ3D.UI.Extensions;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.DependencyInjection;
    using MediatR;
    using Microsoft.AspNetCore.HttpOverrides;

    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }


        public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
          .ConfigureWebHostDefaults(webBuilder =>
          {
              webBuilder
              .UseUrls("http://*:4773")
              .UseStartup<Startup>();
          });
    }


    public class Startup
    {
        /*
         * mysql��sqlserver��Ǩ�Ʋ�������һ�£���������Ŀ��Ǩ���ļ��Ѿ�Ǩ�ƺã���Data�ļ����£�
         * msqlʹ��MigrationsMySql�ļ����µ�Ǩ�Ƽ�¼��ж����һ��Migrations�ļ���
         * sqlserverʹ��Migrations�ļ����µ�Ǩ�Ƽ�¼��ж����һ��MigrationsMySql�ļ���
         * 
         * ��Ȼ��Ҳ���Զ�ɾ�����Լ�������Ǩ�ơ�
         * 
         һ��Ǩ����Ŀ1��һ��Ҫ�л��� Christ3D.Infrastruct ��Ŀ�£�ʹ�� Package Manager Console����
           1��add-migration InitStudentDbMysql -Context StudyContext  -o MigrationsMySql
           2��add-migration InitEventStoreDbMysql -Context EventStoreSQLContext -o MigrationsMySql/EventStore
           3��update-database -Context StudyContext
           4��update-database -Context EventStoreSQLContext

         ����Ǩ����Ŀ2�����ã���Ϊ������ʹ��IdentityServer4����һ��Ҫ�л��� Christ3D.Infrastruct.Identity ��Ŀ�£�ʹ�� Package Manager Console����
           1��add-migration InitIdentityDbMysql -Context ApplicationDbContext -o Data/MigrationsMySql/ 
           2��update-database -Context ApplicationDbContext
             
        */

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //nginx 
            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders =
                    ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });

            services.AddHsts(options =>
            {
                options.Preload = true;
                options.IncludeSubDomains = true;
                options.MaxAge = TimeSpan.FromDays(60);
                options.ExcludedHosts.Add("ddd.neters.com");
            });


            //services.AddSameSiteCookiePolicy();

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            // IdentityServer4 ע��
            //if (Configuration["Authentication:IdentityServer4:Enabled"].ObjToBool())
            //{
            //    System.Console.WriteLine("��ǰ��Ȩģʽ��:Ids4");
            //    services.AddId4OidcSetup(Configuration);
            //}
            //else
            //{
            //    System.Console.WriteLine("��ǰ��Ȩģʽ��:Identity");
            //    services.AddIdentitySetup(Configuration);
            //}

            // Automapper ע��
            services.AddAutoMapperSetup();

            services.AddControllersWithViews();

            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("CanWriteStudentData", policy => policy.Requirements.Add(new ClaimRequirement("Students", "Write")));
            //    options.AddPolicy("CanRemoveStudentData", policy => policy.Requirements.Add(new ClaimRequirement("Students", "Remove")));
            //    options.AddPolicy("CanWriteOrRemoveStudentData", policy => policy.Requirements.Add(new ClaimRequirement("Students", "WriteOrRemove")));
            //});

            // Adding MediatR for Domain Events
            // ������������¼���ע��
            // ���ð� MediatR.Extensions.Microsoft.DependencyInjection
            services.AddMediatR(typeof(Startup));

            // .NET Core ԭ������ע��
            // ��дһ����������������չʾ�� Presentation �и���
            NativeInjectorBootStrapper.RegisterServices(services);


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCookiePolicy();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Student}/{action=Index}/{id?}");
            });
        }
    }
}
