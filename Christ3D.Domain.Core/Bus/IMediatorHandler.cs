using Christ3D.Domain.Core.Commands;
using Christ3D.Domain.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Christ3D.Domain.Core.Bus
{
    /// <summary>
    /// 中介处理程序接口
    /// 可以定义多个处理程序
    /// 是异步的
    /// </summary>
    public interface IMediatorHandler
    {
        /// <summary>
        /// 发送命令，将我们的命令模型发布到中介者模块
        /// </summary>
        Task SendCommand<T>(T command) where T : Command;


        /// <summary>
        /// 引发事件，通过总线，发布事件
        /// </summary>
        Task RaiseEvent<T>(T @event) where T : Event;
    }
}
