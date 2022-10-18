using System.Net;
using System.Net.Http.Headers;
using GeekShopping.Web.Extensions;
using GeekShopping.Web.Models;
using GeekShopping.Web.Services.IServices;

namespace GeekShopping.Web.Services; 

public class CouponService : ICouponService {
    private const string BasePath = "api/v1/coupon";
    private readonly HttpClient _client;

    public CouponService(HttpClient client)
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));
    }
    
    public async Task<CouponViewModel> GetCoupon(string code, string accessToken) {
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",accessToken);
        var response = await _client.GetAsync($"{BasePath}/{code}");
        if (response.StatusCode != HttpStatusCode.OK) return new CouponViewModel();
        return await response.ReadContentAs<CouponViewModel>();
    }
}