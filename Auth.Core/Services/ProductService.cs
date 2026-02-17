using Auth.Core.Common;
using Auth.Core.DTOs;
using Auth.Core.Entities;
using Auth.Core.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Auth.Core.Services
{
    public class ProductService
    {
        private readonly IProductsRepository _repo;
        private readonly ICategoriesRepository _categoriesRepository;


        public ProductService(IProductsRepository repo, ICategoriesRepository categoriesRepository)
        {
            _repo = repo;
            _categoriesRepository = categoriesRepository;
        }



        public async Task<ProductsResponse> GetProudctsAsync(string? name,
    int? categoryId)
        {
            var products = await _repo.GetProudctsAsync(name, categoryId);
            return ProductsResponse.Success(products);
        }

        public async Task<Result<Product>> GetByIdAsync(Guid id)
        {
            var product = await _repo.GetByIdAsync(id);
            if (product == null)
                return Result<Product>.Fail("Product not found");

            return Result<Product>.Ok(product);
        }

        public async Task<ProductCreateResponse> CreateAsync(CreateProductRequest productReq)
        {
            if (string.IsNullOrWhiteSpace(productReq.ProductName))
                return ProductCreateResponse.Fail("Product name is required");

            if (productReq.Price < 0)
                return ProductCreateResponse.Fail("Price must be >= 0");

            if (productReq.UnitsInStock < 0)
                return ProductCreateResponse.Fail("Stock must be >= 0");

            var product = new Product(
                productReq.ProductName,
                productReq.CategoryId,
                productReq.Price,
                productReq.UnitsInStock
            );

            await _repo.AddAsync(product);

            return ProductCreateResponse.Success(product);
        }

        public async Task<Result<Product>> UpdateAsync(Product product)
        {
            var existing = await _repo.GetByIdAsync(product.Id);
            if (existing == null)
                return Result<Product>.Fail("Product not found");

            if (string.IsNullOrWhiteSpace(product.ProductName))
                return Result<Product>.Fail("Product name is required");

            if (product.Price < 0)
                return Result<Product>.Fail("Price must be >= 0");

            if (product.UnitsInStock < 0)
                return Result<Product>.Fail("Stock must be >= 0");

            await _repo.UpdateAsync(product);

            return Result<Product>.Ok(product);
        }

        public async Task<BaseResponse> DeleteAsync(Guid id)
        {
            var existing = await _repo.GetByIdAsync(id);

            if (existing == null)
            {
                return BaseResponse.Fail("Product not found");
            }


            await _repo.DeleteAsync(existing);

            return BaseResponse.Success();
        }


        public async Task<ProductCreateResponse> UpdateAsync(Guid id, UpdateProductRequest req)
        {
            var existing = await _repo.GetByIdAsync(id);

            if (existing == null)
                return ProductCreateResponse.Fail("Product not found");

            if (string.IsNullOrWhiteSpace(req.ProductName))
                return ProductCreateResponse.Fail("Product name is required");

            if (req.Price < 0)
                return ProductCreateResponse.Fail("Price must be >= 0");

            if (req.UnitsInStock < 0)
                return ProductCreateResponse.Fail("Stock must be >= 0");

            existing.ProductName = req.ProductName;
            existing.CategoryId = req.CategoryId;
            existing.Price = req.Price;
            existing.UnitsInStock = req.UnitsInStock;
            existing.UpdatedAt = DateTime.UtcNow;

            await _repo.UpdateAsync(existing);

            return ProductCreateResponse.Success(existing);
        }

        public async Task<ProductFormResponse> GetProductFormDataAsync()
        {
            var categories = await _categoriesRepository.GetAllCategoriesAsync();
            return new ProductFormResponse
            {
                Categories = categories
                
            };

        }
    }
}
