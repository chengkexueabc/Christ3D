using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Christ3D.Domain.Core.Events
{
    /// <summary>
    /// 领域存储服务接口
    /// </summary>
    public interface IEventStoreService
    {
        /// <summary>
        /// 将命令模型进行保存
        /// </summary>
        void Save<T>(T theEvent) where T : Event;
    }
}
