using Contracts.Domains;
using Ordering.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ordering.Domain.Entities
{
    public class Order : EntityAuditBase<long>
    {
        [Required]
        public required string UserName { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal TotalPrice { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(150)")]
        public required string FirstName { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(150)")]
        public required string LastName { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(250)")]
        public required string EmailAddress { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(max)")]
        public string? ShippingAddress { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(max)")]
        public string? InvoiceAddress { get; set; }

        public EOrderStatus Status { get; set; }
    }
}
