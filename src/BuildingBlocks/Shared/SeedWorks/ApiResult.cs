using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.SeedWorks
{
    public class ApiResult<T>
    {
        public bool IsSuccessed { get; set; }

        public string? Message { get; set; }

        public T? Data { get; set; }

        public ApiResult()
        {
        }

        public ApiResult(bool isSuccessed, string? message = null)
        {
            IsSuccessed = isSuccessed;
            Message = message;
        }

        public ApiResult(bool isSuccessed, T data, string? message = null)
        {
            IsSuccessed = isSuccessed;
            Message = message;
            Data = data;
        }
    }
}
