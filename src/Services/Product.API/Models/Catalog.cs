using Contracts.Domains;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Product.API.Models
{
    public class Catalog : EntityAuditBase<long>
    {
        [Required]
        [Column(TypeName = "varchar(150)")]
        public required string No { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(250)")]
        public required string Name { get; set; }

        [Column(TypeName = "nvarchar(450)")]
        public string? Summary { get; set; }

        [Column(TypeName = "text")]
        public string? Description { get; set; }


        [Column(TypeName = "decimal(12, 2)")]
        public decimal Price { get; set; }
    }
}
