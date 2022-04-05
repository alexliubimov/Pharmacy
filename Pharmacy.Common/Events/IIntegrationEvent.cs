using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Common.Events
{
    public interface IIntegrationEvent
    {
        Guid Id { get; }
    }
}
