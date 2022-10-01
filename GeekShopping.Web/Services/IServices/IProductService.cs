﻿using GeekShopping.Web.Models;

namespace GeekShopping.Web.Services.IServices;

public interface IProductService
{
    Task<IEnumerable<ProductViewModel>> FindAllProducts(string token);
    Task<ProductViewModel> FindProductById(long id, string token);
    Task<ProductViewModel> CreateProduct(ProductViewModel viewModel, string token);
    Task<ProductViewModel> UpdateProduct(ProductViewModel viewModel, string token);
    Task<bool> DeleteProductById(long id, string token);
}