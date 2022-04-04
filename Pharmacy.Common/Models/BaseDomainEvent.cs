using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Common.Models
{
    public abstract record BaseDomainEvent<TA, TKey> : IDomainEvent<TKey>
        where TA : IAggregateRoot<TKey>
    {
        protected BaseDomainEvent() { }

        protected BaseDomainEvent(TA aggregateRoot)
        {
            if (aggregateRoot is null)
                throw new ArgumentNullException(nameof(aggregateRoot));

            this.AggregateVersion = aggregateRoot.Version;
            this.AggregateId = aggregateRoot.Id;
            this.When = DateTime.UtcNow;
        }

        public long AggregateVersion { get; private set; }
        public TKey AggregateId { get; private set; }
        public DateTime When { get; private set; }
    }
}
