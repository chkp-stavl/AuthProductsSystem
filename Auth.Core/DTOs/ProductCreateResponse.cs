using Auth.Core.Common;
using Auth.Core.Entities;

namespace Auth.Core.DTOs
{
    public class ProductCreateResponse : BaseResponse
    {
        public Product? Data { get; }

        private ProductCreateResponse(bool isSuccess, Product? product = null, string? message = null)
            : base(isSuccess, message)
        {
            Data = product;
        }

        public static ProductCreateResponse Success(Product product)
            => new(true, product);

        public static ProductCreateResponse Fail(string message)
            => new(false, null, message);
    }
}
