using EventStore.ClientAPI;

namespace Pharmacy.EventStore
{
    public interface IEventStoreConnectionWrapper
    {
        Task<IEventStoreConnection> GetConnectionAsync();
    }
}
