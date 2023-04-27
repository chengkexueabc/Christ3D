using Christ3D.Domain.Core.Events;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Christ3D.Application.EventSourcing
{
    /// <summary>
    /// 事件存储服务类
    /// </summary>
    public class SqlEventStoreService : IEventStoreService
    {
        public void Save<T>(T theEvent) where T : Event
        {
            // 对事件模型序列化
            var serializedData = JsonConvert.SerializeObject(theEvent);

            //var storedEvent = new StoredEvent(
            //    theEvent,
            //    serializedData,
            //    _user.Name);

            //_eventStoreRepository.Store(storedEvent);
        }
    }
}
