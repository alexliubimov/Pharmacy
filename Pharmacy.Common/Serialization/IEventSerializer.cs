using Pharmacy.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Common.Serialization
{
    public interface IEventSerializer
    {
        IDomainEvent<TKey> Deserialize<TKey>(string type, byte[] data);
        IDomainEvent<TKey> Deserialize<TKey>(string type, string data);
        byte[] Serialize<TKey>(IDomainEvent<TKey> @event);
    }
}
