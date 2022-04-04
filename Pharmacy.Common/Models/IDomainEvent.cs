using System;
using System.Collections.Generic;
using System.Text;

namespace Pharmacy.Common.Models
{
    public interface IDomainEvent<TKey>
    {
        long AggregateVersion { get; }
        TKey AggregateId { get; }
        DateTime When { get; }
    }
}
