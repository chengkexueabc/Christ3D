using Christ3D.Domain.Events.Student;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Christ3D.Domain.EventHandlers
{
    public class StudentEventHandler :
        INotificationHandler<StudentRegisteredEvent>,
        INotificationHandler<StudentUpdatedEvent>,
        INotificationHandler<StudentRemovedEvent>
    {
        // 学习被注册成功后的事件处理方法
        public Task Handle(StudentRegisteredEvent message, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        // 学生被修改成功后的事件处理方法
        public Task Handle(StudentUpdatedEvent message, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        // 学习被删除后的事件处理方法
        public Task Handle(StudentRemovedEvent message, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
