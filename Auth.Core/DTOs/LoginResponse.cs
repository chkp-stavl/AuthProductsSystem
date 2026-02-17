using Auth.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Core.DTOs
{

    public class LoginResponse : BaseResponse
    {
        public Guid Id { get; private set; }
        public string UserName { get; private set; } = "";
        public bool IsCanEdit { get; private set; }
        public string? Token { get; private set; }

        private LoginResponse(bool success, string? message = null)
            : base(success, message) { }

        public static LoginResponse Success(Guid id, string userName, bool isCanEdit, string token)
        {
            return new LoginResponse(true)
            {
                Id = id,
                UserName = userName,
                IsCanEdit = isCanEdit,
                Token =  token
            };
        }

        public static LoginResponse Fail(string message)
        {
            return new LoginResponse(false, message)
            {
                Token = null
            };
        }
    }
}
