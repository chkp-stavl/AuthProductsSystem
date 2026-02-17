using Auth.Core.Common;

namespace Auth.Core.DTOs
{

    public class LoginResponse : BaseResponse
    {
        public Guid Id { get; private set; }
        public string UserName { get; private set; } = "";
        public bool IsCanEdit { get; private set; }
        public string? Token { get; private set; }

        private LoginResponse(bool isSuccess, string? message = null)
            : base(isSuccess, message) { }

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
