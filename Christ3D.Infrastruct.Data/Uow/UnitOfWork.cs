﻿using Christ3D.Domain.Interfaces;
using Christ3D.Infrastruct.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Christ3D.Infrastruct.Data.Uow
{
    public class UnitOfWork : IUnitOfWork
    {
        //数据库上下文
        private readonly StudyContext _context;

        //构造函数注入
        public UnitOfWork(StudyContext context)
        {
            _context = context;
        }

        //上下文提交
        public bool Commit()
        {
            return _context.SaveChanges() > 0;
        }

        //手动回收
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
