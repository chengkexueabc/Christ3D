using Christ3D.Domain.Core.Bus;
using Christ3D.Domain.Core.Commands;
using Christ3D.Domain.Core.Events;
using MediatR;
using MediatR.Internal;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Christ3D.Infrastruct.Data.Bus
{
    public sealed class InMemoryBus : IMediatorHandler
    {
        private readonly IMediator _mediator;

        private static readonly ConcurrentDictionary<Type, object> _requestHandlers = new ConcurrentDictionary<Type, object>();

        private readonly IEventStoreService _eventStoreService;


        public InMemoryBus(IMediator mediator, IEventStoreService eventStoreService)
        {
            _mediator = mediator;
            _eventStoreService = eventStoreService;
        }

        public Task SendCommand<T>(T command) where T : Command
        {
            return _mediator.Send(command);//请注意 入参 的类型
        }

        public Task RaiseEvent<T>(T @event) where T : Event
        {
            if (!@event.MessageType.Equals("DomainNotification"))
                _eventStoreService?.Save(@event);

            // MediatR中介者模式中的第二种方法，发布/订阅模式
            return _mediator.Publish(@event);
        }


    }
}
