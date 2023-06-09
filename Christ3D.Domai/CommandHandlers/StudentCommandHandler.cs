﻿using Christ3D.Domain.Commands.Student;
using Christ3D.Domain.Core.Bus;
using Christ3D.Domain.Core.Notifications;
using Christ3D.Domain.Events.Student;
using Christ3D.Domain.Interfaces;
using Christ3D.Domain.Models;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Christ3D.Domain.CommandHandlers
{
    public class StudentCommandHandler : CommandHandler,
        IRequestHandler<RegisterStudentCommand, bool>,
        IRequestHandler<UpdateStudentCommand, bool>,
        IRequestHandler<RemoveStudentCommand, bool>
    {
        // 注入仓储接口
        private readonly IStudentRepository _studentRepository;
        // 注入总线
        private readonly IMediatorHandler Bus;
        private IMemoryCache Cache;

        public StudentCommandHandler(IStudentRepository studentRepository,
                                      IUnitOfWork uow,
                                      IMediatorHandler bus,
                                      IMemoryCache cache
                                      ) : base(uow, bus, cache)
        {
            _studentRepository = studentRepository;
            Bus = bus;
            Cache = cache;
        }


        // RegisterStudentCommand命令的处理程序
        public Task<bool> Handle(RegisterStudentCommand message, CancellationToken cancellationToken)
        {
            // 命令验证
            if (!message.IsValid())
            {
                // 错误信息收集
                NotifyValidationErrors(message);
                // 返回，结束当前线程
                return Task.FromResult(false);
            }

            var address = new Address(message.Province, message.City,
            message.County, message.Street);
            var student = new Student(Guid.NewGuid(), message.Name, message.Email, message.Phone, message.BirthDate, address);

            if (_studentRepository.GetByEmail(student.Email) != null)
            {
                //引发错误事件
                Bus.RaiseEvent(new DomainNotification("", "该邮箱已经被使用！"));
                return Task.FromResult(false);
            }

            // 持久化
            _studentRepository.Add(student);

            // 统一提交
            if (Commit())
            {
                // 提交成功后，这里需要发布领域事件
                // 比如欢迎用户注册邮件呀，短信呀等

                Bus.RaiseEvent(new StudentRegisteredEvent(student.Id, student.Name, student.Email, student.BirthDate, student.Phone));
            }

            return Task.FromResult(true);


        }

        // UpdateStudentCommand 的处理方法
        public Task<bool> Handle(UpdateStudentCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Task.FromResult(false);

            }


            var address = new Address(message.Province, message.City,
            message.County, message.Street);
            var student = new Student(message.Id, message.Name, message.Email, message.Phone, message.BirthDate, address);
            var existingStudent = _studentRepository.GetByEmail(student.Email);

            if (existingStudent != null && existingStudent.Id != student.Id)
            {
                if (!existingStudent.Equals(student))
                {

                    Bus.RaiseEvent(new DomainNotification("", "该邮箱已经被使用！"));
                    return Task.FromResult(false);

                }
            }

            _studentRepository.Update(student);

            if (Commit())
            {

                Bus.RaiseEvent(new StudentUpdatedEvent(student.Id, student.Name, student.Email, student.BirthDate, student.Phone));
            }

            return Task.FromResult(true);

        }

        // RemoveStudentCommand 的处理方法
        public Task<bool> Handle(RemoveStudentCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Task.FromResult(false);

            }

            _studentRepository.Remove(message.Id);

            if (Commit())
            {
                Bus.RaiseEvent(new StudentRemovedEvent(message.Id));
            }

            return Task.FromResult(true);

        }

        // 手动回收
        public void Dispose()
        {
            _studentRepository.Dispose();
        }
    }
}
