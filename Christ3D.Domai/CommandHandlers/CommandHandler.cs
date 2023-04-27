using Christ3D.Domain.Core.Bus;
using Christ3D.Domain.Core.Commands;
using Christ3D.Domain.Core.Notifications;
using Christ3D.Domain.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Christ3D.Domain.CommandHandlers
{
    /// <summary>
    /// 领域命令处理程序
    /// </summary>
    public class CommandHandler
    {
        // 注入工作单元
        private readonly IUnitOfWork _uow;
        // 注入中介处理接口
        private readonly IMediatorHandler _bus;
        // 注入缓存，用来存储错误信息
        private IMemoryCache _cache;

        public CommandHandler(IUnitOfWork uow, IMediatorHandler bus, IMemoryCache cache)
        {
            _uow = uow;
            _bus = bus;
            _cache = cache;
        }

        protected void NotifyValidationErrors(Command message)
        {
            List<string> errorInfo = new List<string>();
            foreach (var error in message.ValidationResult.Errors)
            {
                _bus.RaiseEvent(new DomainNotification("", error.ErrorMessage));
            }

        }


        //工作单元提交
        //如果有错误，下一步会在这里添加领域通知
        public bool Commit()
        {
            if (_uow.Commit()) return true;

            return false;
        }
    }
}
