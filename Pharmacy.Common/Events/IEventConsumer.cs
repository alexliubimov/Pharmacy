using System;
using System.Threading;
using System.Threading.Tasks;

namespace Pharmacy.Common.Events
{
    public interface IEventConsumer
    {
        Task StartConsumeAsync(CancellationToken cancellationToken = default);

        event EventReceivedHandler EventReceived;
        event ExceptionThrownHandler ExceptionThrown;
    }

    public delegate Task EventReceivedHandler(object sender, IIntegrationEvent @event);
    public delegate void ExceptionThrownHandler(object sender, Exception e);
}
