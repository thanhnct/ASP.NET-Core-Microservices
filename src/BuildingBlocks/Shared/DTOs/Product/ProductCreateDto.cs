﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs.Product
{
    public class ProductCreateDto
    {
        public required string No { get; set; }

        public required string Name { get; set; }

        public string? Summary { get; set; }

        public string? Description { get; set; }

        public decimal Price { get; set; }
    }
}
