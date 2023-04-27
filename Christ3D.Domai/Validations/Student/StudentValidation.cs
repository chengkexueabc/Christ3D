using Christ3D.Domain.Commands.Student;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Christ3D.Domain.Validations.Student
{
    public abstract class StudentValidation<T> : AbstractValidator<T> where T : StudentCommand
    {
        //受保护方法，验证Name
        protected void ValidateName()
        {
            //定义规则，c 就是当前 StudentCommand 类
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("姓名不能为空")
                .Length(2, 10).WithMessage("姓名在2~10个字符之间");
        }

        //验证年龄
        protected void ValidateBirthDate()
        {
            RuleFor(c => c.BirthDate)
                .NotEmpty()
                .Must(HaveMinimumAge)
                .WithMessage("学生应该14岁以上！");
        }

        //验证邮箱
        protected void ValidateEmail()
        {
            RuleFor(c => c.Email)
                .NotEmpty()
                .EmailAddress();
        }
        //验证手机号
        protected void ValidatePhone()
        {
            RuleFor(c => c.Phone)
                .NotEmpty()
                .Must(HavePhone)
                .WithMessage("手机号应该为11位！");
        }

        //验证Guid
        protected void ValidateId()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty);
        }

        // 表达式
        protected static bool HaveMinimumAge(DateTime birthDate)
        {
            return birthDate <= DateTime.Now.AddYears(-14);
        }
        // 表达式
        protected static bool HavePhone(string phone)
        {
            return phone.Length == 11;
        }
    }
}
