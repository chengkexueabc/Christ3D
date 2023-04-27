using AutoMapper;
using Christ3D.Application.Interfaces;
using Christ3D.Application.ViewModels;
using Christ3D.Domain.Commands.Student;
using Christ3D.Domain.Core.Bus;
using Christ3D.Domain.Interfaces;
using Christ3D.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Christ3D.Application.Services
{
    /// <summary>
    /// StudentAppService 服务接口实现类,继承 服务接口
    /// 通过 DTO 实现视图模型和领域模型的关系处理
    /// 作为调度者，协调领域层和基础层，
    /// 这里只是做一个面向用户用例的服务接口,不包含业务规则或者知识
    /// </summary>
    public class StudentAppService : IStudentAppService
    {
        // 注意这里是要IoC依赖注入的，还没有实现
        private readonly IStudentRepository _StudentRepository;
        // 用来进行DTO
        private readonly IMapper _mapper;
        // 中介者 总线
        private readonly IMediatorHandler Bus;

        public StudentAppService(
            IStudentRepository StudentRepository,
            IMapper mapper,
            IMediatorHandler bus)
        {
            _StudentRepository = StudentRepository;
            _mapper = mapper;
            Bus = bus;
        }

        public IEnumerable<StudentViewModel> GetAll()
        {
            var all = _StudentRepository.GetAll();
            return _mapper.Map<IEnumerable<StudentViewModel>>(all);
        }

        public StudentViewModel GetById(Guid id)
        {
            return _mapper.Map<StudentViewModel>(_StudentRepository.GetById(id));
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public void Register(StudentViewModel studentViewModel)
        {
            var registerCommand = _mapper.Map<RegisterStudentCommand>(studentViewModel);
            Bus.SendCommand(registerCommand);
        }

        public void Update(StudentViewModel studentViewModel)
        {
            var updateCommand = _mapper.Map<UpdateStudentCommand>(studentViewModel);
            Bus.SendCommand(updateCommand);
        }

        public void Remove(Guid id)
        {
            var removeCommand = new RemoveStudentCommand(id);
            Bus.SendCommand(removeCommand);
        }

    }
}
