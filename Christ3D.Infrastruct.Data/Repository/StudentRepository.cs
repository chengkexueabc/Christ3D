using AutoMapper;
using Christ3D.Application.Interfaces;
using Christ3D.Application.ViewModels;
using Christ3D.Domain.Interfaces;
using Christ3D.Domain.Models;
using Christ3D.Infrastruct.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Christ3D.Infrastruct.Data.Repository
{
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        public StudentRepository(StudyContext context)
          : base(context)
        {

        }
        //对特例接口进行实现
        public Student GetByEmail(string email)
        {
            return DbSet.AsNoTracking().FirstOrDefault(c => c.Email == email);
        }
    }
}
