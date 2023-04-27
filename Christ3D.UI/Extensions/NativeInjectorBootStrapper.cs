

using Christ3D.Application.EventSourcing;
using Christ3D.Application.Interfaces;
using Christ3D.Application.Services;
using Christ3D.Domain.CommandHandlers;
using Christ3D.Domain.Commands.Student;
using Christ3D.Domain.Core.Bus;
using Christ3D.Domain.Core.Commands;
using Christ3D.Domain.Core.Events;
using Christ3D.Domain.Core.Notifications;
using Christ3D.Domain.EventHandlers;
using Christ3D.Domain.Events.Student;
using Christ3D.Domain.Interfaces;
using Christ3D.Infrastruct.Data.Bus;
using Christ3D.Infrastruct.Data.Context;
using Christ3D.Infrastruct.Data.Repository;
using Christ3D.Infrastruct.Data.Uow;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;

namespace Christ3D.UI.Extensions
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {

            // ASP.NET HttpContext dependency
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            // ASP.NET Authorization Polices

            // 注入 应用层Application
            services.AddScoped<IStudentAppService, StudentAppService>();

            // 命令总线Domain Bus (Mediator)
            services.AddScoped<IMediatorHandler, InMemoryBus>();

            // 领域通知
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
            // 领域事件
            services.AddScoped<INotificationHandler<StudentRegisteredEvent>, StudentEventHandler>();
            services.AddScoped<INotificationHandler<StudentUpdatedEvent>, StudentEventHandler>();
            services.AddScoped<INotificationHandler<StudentRemovedEvent>, StudentEventHandler>();


            // 领域层 - 领域命令
            services.AddScoped<IRequestHandler<RegisterStudentCommand, bool>, StudentCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateStudentCommand, bool>, StudentCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveStudentCommand, bool>, StudentCommandHandler>();

            services.AddSingleton<IMemoryCache>(factory =>
            {
                var cache = new MemoryCache(new MemoryCacheOptions());
                return cache;
            });


            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<StudyContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IEventStoreService, SqlEventStoreService>();


        }
    }
}
