using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Messages
{
    public record IntegrationBaseEvent() : IIntegrationBaseEvent
    {
        public Guid Id { get; init; }

        public DateTime CreationDate { get; init; } = DateTime.UtcNow;
    }
}
