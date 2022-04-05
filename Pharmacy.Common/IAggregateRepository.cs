using Pharmacy.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Pharmacy.Common
{
    public interface IAggregateRepository<TA, TKey>
        where TA : class, IAggregateRoot<TKey>
    {
        Task PersistAsync(TA aggregateRoot, CancellationToken cancellationToken = default);
        Task<TA> RehydrateAsync(TKey key, CancellationToken cancellationToken = default);
    }
}
