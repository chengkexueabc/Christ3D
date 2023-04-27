using AutoMapper;
using Christ3D.Application.ViewModels;
using Christ3D.Domain.Commands.Student;
using Christ3D.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Christ3D.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<StudentViewModel, Student>()
             .ForPath(d => d.Address.Province, o => o.MapFrom(s => s.Province))
             .ForPath(d => d.Address.City, o => o.MapFrom(s => s.City))
             .ForPath(d => d.Address.County, o => o.MapFrom(s => s.County))
             .ForPath(d => d.Address.Street, o => o.MapFrom(s => s.Street));

            CreateMap<StudentViewModel, RegisterStudentCommand>()
                 .ConstructUsing(c => new RegisterStudentCommand(c.Name, c.Email, c.BirthDate, c.Phone, c.Province, c.City,
                 c.County, c.Street));
        }
    }
}
