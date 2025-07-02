using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Messages.IntegrationEvents.Interfaces
{
    public interface IBacketCheckoutEvent : IIntegrationBaseEvent
    {
        decimal TotalPrice { get; set; }

        string? UserName { get; set; }

        string? FirstName { get; set; }

        string? LastName { get; set; }

        string? ShippingAddress { get; set; }

        string? InvoiceAddress { get; set; }
    }
}
