using Auth.Core.Common;
using Auth.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Core.DTOs
{
    public class ProductCreateResponse : BaseResponse
    {
        public Product? Data { get; }

        private ProductCreateResponse(bool success, Product? product = null, string? message = null)
            : base(success, message)
        {
            Data = product;
        }

        public static ProductCreateResponse Success(Product product)
            => new(true, product);

        public static ProductCreateResponse Fail(string message)
            => new(false, null, message);
    }
}
