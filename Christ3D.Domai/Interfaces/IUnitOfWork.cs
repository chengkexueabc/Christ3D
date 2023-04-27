using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Christ3D.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        //是否提交成功
        bool Commit();
    }
}
