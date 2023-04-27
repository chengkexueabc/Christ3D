using Christ3D.Domain.Commands.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Christ3D.Domain.Validations.Student
{
    public class RemoveStudentCommandValidation : StudentValidation<RemoveStudentCommand>
    {
        public RemoveStudentCommandValidation()
        {
            ValidateId();
        }
    }
}
