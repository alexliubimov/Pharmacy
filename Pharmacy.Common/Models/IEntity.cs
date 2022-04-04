using System;
using System.Collections.Generic;
using System.Text;

namespace Pharmacy.Common.Models
{
    public interface IEntity<TKey>
    {
        TKey Id { get; }
    }
}
