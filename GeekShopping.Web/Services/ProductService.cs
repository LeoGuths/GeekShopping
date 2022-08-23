using GeekShopping.Web.Extensions;
using GeekShopping.Web.Models;
using GeekShopping.Web.Services.IServices;

namespace GeekShopping.Web.Services;

public class ProductService : IProductService
{
    public const string BasePath = "api/v1/product";
    private readonly HttpClient _client;

    public ProductService(HttpClient client)
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));
    }

    public async Task<IEnumerable<ProductModel>> FindAllProducts()
    {
        var response = await _client.GetAsync(BasePath);
        return await response.ReadContentAs<List<ProductModel>>();
    }

    public async Task<ProductModel> FindProductById(long id)
    {
        var response = await _client.GetAsync($"{BasePath}/{id}");
        return await response.ReadContentAs<ProductModel>();
    }

    public async Task<ProductModel> CreateProduct(ProductModel model)
    {
        var response = await _client.PostAsJson(BasePath, model);
        return response.IsSuccessStatusCode ? await response.ReadContentAs<ProductModel>() : 
            throw new Exception("Something went wrong when calling API");
    }

    public async Task<ProductModel> UpdateProduct(ProductModel model)
    {
        var response = await _client.PutAsJson(BasePath, model);
        return response.IsSuccessStatusCode ? await response.ReadContentAs<ProductModel>() : 
            throw new Exception("Something went wrong when calling API");
    }

    public async Task<bool> DeleteProductById(long id)
    {
        var response = await _client.DeleteAsync($"{BasePath}/{id}");
        return response.IsSuccessStatusCode ? await response.ReadContentAs<bool>() : throw new Exception("Something went wrong when calling API");
    }
}