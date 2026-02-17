namespace Auth.Core.Common
{

    public class BaseResponse
    {
        public bool IsSuccess { get; }
        public string? Message { get; }

        protected BaseResponse(bool isSuccess, string? message = null)
        {
            IsSuccess = isSuccess;
            Message = message;
        }

        public static BaseResponse Success() => new(true);

        public static BaseResponse Fail(string message) => new(false, message);
    }
}
