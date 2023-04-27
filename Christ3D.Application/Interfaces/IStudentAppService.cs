using Christ3D.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Christ3D.Application.Interfaces
{
    public interface IStudentAppService : IDisposable
    {
        void Register(StudentViewModel studentViewModel);
        IEnumerable<StudentViewModel> GetAll();
        StudentViewModel GetById(Guid id);
        void Update(StudentViewModel studentViewModel);
        void Remove(Guid id);
    }
}
