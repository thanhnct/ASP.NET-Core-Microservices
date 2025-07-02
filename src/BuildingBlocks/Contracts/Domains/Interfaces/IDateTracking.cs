using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Domains.Interfaces
{
    public interface IDateTracking
    {
        DateTimeOffset CreatedAt { get; set; }

        DateTimeOffset? LastModifiedAt { get; set; }
    }
}
