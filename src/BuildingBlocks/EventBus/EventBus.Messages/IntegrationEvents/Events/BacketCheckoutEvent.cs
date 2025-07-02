using EventBus.Messages.IntegrationEvents.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Messages.IntegrationEvents.Events
{
    public record BacketCheckoutEvent() : IntegrationBaseEvent, IBacketCheckoutEvent
    {
        public decimal TotalPrice { get; set; }
        public string? UserName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? ShippingAddress { get; set; }
        public string? InvoiceAddress { get; set; }
    }
}
