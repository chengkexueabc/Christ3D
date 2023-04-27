using Christ3D.Domain.Commands.Student;
using Christ3D.Domain.Validations.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Christ3D.Domain.Validations
{
    public class UpdateStudentCommandValidation : StudentValidation<UpdateStudentCommand>
    {
        public UpdateStudentCommandValidation()
        {
            ValidateId();
            ValidateName();
            ValidateBirthDate();
            ValidateEmail();
            ValidatePhone();
        }
    }
}
