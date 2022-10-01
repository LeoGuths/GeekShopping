using GeekShopping.Web.Models;

namespace GeekShopping.Web.Services.IServices; 

public interface ICartService {
    Task<CartViewModel> FindCartByUserId(string userId, string accessToken);
    Task<CartViewModel> AddItemToCart(CartViewModel model, string accessToken);
    Task<CartViewModel> UpdateCart(CartViewModel model, string accessToken);
    Task<bool> RemoveFromCart(long cartId, string accessToken);
    Task<bool> ApplyCoupon(CartViewModel cart, string couponCode, string accessToken);
    Task<bool> RemoveCoupon(string userId, string accessToken);
    Task<bool> ClearCart(string userId, string accessToken);
    Task<CartViewModel> Checkout(CartHeaderViewModel cartHeader, string accessToken);
}