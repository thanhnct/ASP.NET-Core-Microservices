using Contracts.Domains;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Customer.API.Models
{
    public class Customer : EntityBase<int>
    {
        [Required]
        [Column(TypeName = "varchar(150)")]
        public required string UserName { get; set; }

        [Required]
        [Column(TypeName = "varchar(150)")]
        public required string FirstName { get; set; }

        [Required]
        [Column(TypeName = "varchar(150)")]
        public required string LastName { get; set; }

        [Required]
        [Column(TypeName = "varchar(250)")]
        [EmailAddress]
        public required string Email { get; set; }
    }
}
