using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Pharmacy.Common.Events
{
    public interface IEventProducer
    {
        Task DispatchAsync(IIntegrationEvent @event, CancellationToken cancellationToken = default);
    }
}
