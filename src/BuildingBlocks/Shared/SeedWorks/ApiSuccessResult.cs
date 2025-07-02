using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.SeedWorks
{
    public class ApiSuccessResult<T> : ApiResult<T>
    {
        public ApiSuccessResult(T data, string? message = null) : base(true, data, message)
        {

        }
    }
}
