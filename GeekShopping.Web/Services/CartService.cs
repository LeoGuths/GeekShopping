using System.Net.Http.Headers;
using GeekShopping.Web.Extensions;
using GeekShopping.Web.Models;
using GeekShopping.Web.Services.IServices;

namespace GeekShopping.Web.Services; 

public class CartService : ICartService {
    public const string BasePath = "api/v1/cart";
    private readonly HttpClient _client;

    public CartService(HttpClient client)
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));
    }
    
    public async Task<CartViewModel> FindCartByUserId(string userId, string accessToken) {
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",accessToken);
        var response = await _client.GetAsync($"{BasePath}/find-cart/{userId}");
        return await response.ReadContentAs<CartViewModel>();
    }

    public async Task<CartViewModel> AddItemToCart(CartViewModel model, string accessToken) {
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",accessToken);
        var response = await _client.PostAsJson($"{BasePath}/add-cart", model);
        return response.IsSuccessStatusCode ? await response.ReadContentAs<CartViewModel>() : 
            throw new Exception("Something went wrong when calling API");
    }

    public async Task<CartViewModel> UpdateCart(CartViewModel model, string accessToken) {
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",accessToken);
        try {
            var response = await _client.PutAsJson($"{BasePath}/update-cart", model);
            return response.IsSuccessStatusCode 
                ? await response.ReadContentAs<CartViewModel>() 
                : throw new Exception("Something went wrong when calling API");
        }
        catch (Exception e) {
            throw new Exception("Something went wrong when calling API", e);
        }
    }

    public async Task<bool> RemoveFromCart(long cartId, string accessToken) {
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",accessToken);
        var response = await _client.DeleteAsync($"{BasePath}/remove-cart/{cartId}");
        return response.IsSuccessStatusCode ? await response.ReadContentAs<bool>() : throw new Exception("Something went wrong when calling API");
    }

    public async Task<bool> ApplyCoupon(CartViewModel model, string accessToken) {
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",accessToken);
        var response = await _client.PostAsJson($"{BasePath}/apply-coupon", model);
        return response.IsSuccessStatusCode ? await response.ReadContentAs<bool>() : throw new Exception("Something went wrong when calling API");
    }

    public async Task<bool> RemoveCoupon(string userId, string accessToken) {
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",accessToken);
        var response = await _client.DeleteAsync($"{BasePath}/remove-coupon/{userId}");
        return response.IsSuccessStatusCode ? await response.ReadContentAs<bool>() : throw new Exception("Something went wrong when calling API");
    }

    public async Task<bool> ClearCart(string userId, string accessToken) {
        throw new NotImplementedException();
    }

    public async Task<CartViewModel> Checkout(CartHeaderViewModel cartHeader, string accessToken) {
        throw new NotImplementedException();
    }
}