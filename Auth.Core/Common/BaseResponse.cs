using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Core.Common
{

    public class BaseResponse
    {
        public bool IsSuccess { get; }
        public string? Message { get; }

        protected BaseResponse(bool success, string? message = null)
        {
            IsSuccess = success;
            Message = message;
        }

        public static BaseResponse Success() => new(true);

        public static BaseResponse Fail(string message) => new(false, message);
    }
}
