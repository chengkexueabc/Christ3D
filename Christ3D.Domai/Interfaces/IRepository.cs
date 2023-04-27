using Christ3D.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Christ3D.Domain.Interfaces
{
    /// <summary>
    /// 定义泛型仓储接口，并继承IDisposable，显式释放资源
    /// </summary>
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {

        void Add(TEntity obj);

        TEntity GetById(Guid id);

        IQueryable<TEntity> GetAll();
 
        void Update(TEntity obj);

        void Remove(Guid id);

        int SaveChanges();
    }

    public interface ICustomerRepository : IRepository<Customer>
    {

    }

    public interface IStudentRepository : IRepository<Student>
    {
        Student GetByEmail(string email);
    }
}
