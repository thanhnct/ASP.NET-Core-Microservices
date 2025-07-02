using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs
{
    public class PagedDto<T>
    {
        public PagedDto() => Items = new List<T>();

        public IEnumerable<T> Items { get; set; }

        public int Page { get; set; }

        public int PageSize { get; set; }

        public int TotalCount { get; set; }

        public int TotalPages { get; set; }
    }
}
