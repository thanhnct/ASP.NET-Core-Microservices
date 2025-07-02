using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs.Customer
{
    public class CustomerCreateDto
    {
        public required string UserName { get; set; }

        public required string FirstName { get; set; }

        public required string LastName { get; set; }

        public required string Email { get; set; }
    }
}
