using System;
using System.Collections.Generic;
using System.Text;

namespace Pharmacy.Common.Models
{
    public interface IAggregateRoot<TKey> : IEntity<TKey>
    {
        long Version { get; }
        IReadOnlyCollection<IDomainEvent<TKey>> Events { get; }
        void ClearEvents();
    }
}
