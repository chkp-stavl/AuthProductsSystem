using Auth.Core.Common;

namespace Auth.Core.DTOs
{
    public class ProductsResponse : BaseResponse
    {
        public List<ProductResponse> Data { get; set; } = new();

        private ProductsResponse(bool isSuccess, string? message = null)
        : base(isSuccess, message) { }

        public static ProductsResponse Success(List<ProductResponse> products)
        {
            return new ProductsResponse(true)
            {
                Data = products
            };
        }

        public static ProductsResponse Fail(string message)
        {
            return new ProductsResponse(false, message);
        }
    }
}
