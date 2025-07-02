using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs.Customer
{
    public class CustomerDto
    {
        public long Id { get; set; }

        public required string UserName { get; set; }

        public required string FirstName { get; set; }

        public required string LastName { get; set; }

        public required string Email { get; set; }
    }
}
