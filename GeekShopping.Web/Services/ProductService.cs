using System.Net.Http.Headers;
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

    public async Task<IEnumerable<ProductViewModel>> FindAllProducts(string token) {
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);
        var response = await _client.GetAsync(BasePath);
        return await response.ReadContentAs<List<ProductViewModel>>();
    }

    public async Task<ProductViewModel> FindProductById(long id, string token)
    {
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);
        var response = await _client.GetAsync($"{BasePath}/{id}");
        return await response.ReadContentAs<ProductViewModel>();
    }

    public async Task<ProductViewModel> CreateProduct(ProductViewModel viewModel, string token)
    {
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);
        var response = await _client.PostAsJson(BasePath, viewModel);
        return response.IsSuccessStatusCode ? await response.ReadContentAs<ProductViewModel>() : 
            throw new Exception("Something went wrong when calling API");
    }

    public async Task<ProductViewModel> UpdateProduct(ProductViewModel viewModel, string token)
    {
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);
        var response = await _client.PutAsJson(BasePath, viewModel);
        return response.IsSuccessStatusCode ? await response.ReadContentAs<ProductViewModel>() : 
            throw new Exception("Something went wrong when calling API");
    }

    public async Task<bool> DeleteProductById(long id, string token)
    {
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);
        var response = await _client.DeleteAsync($"{BasePath}/{id}");
        return response.IsSuccessStatusCode ? await response.ReadContentAs<bool>() : throw new Exception("Something went wrong when calling API");
    }
}