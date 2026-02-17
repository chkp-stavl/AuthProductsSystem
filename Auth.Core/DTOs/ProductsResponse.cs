using Auth.Core.Common;
using Auth.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Core.DTOs
{
    public class ProductsResponse : BaseResponse
    {
        public List<ProductResponse> Data { get; set; } = new();

        private ProductsResponse(bool success, string? message = null)
        : base(success, message) { }

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
