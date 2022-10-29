using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;
using GeekShopping.CartAPI.Data.ValueObjects;

namespace GeekShopping.CartAPI.Repository; 

public class CouponRepository : ICouponRepository {
    private readonly HttpClient _client;

    public CouponRepository(HttpClient client) {
        _client = client;
    }

    public async Task<CouponVO> GetCoupon(string couponCode, string accessToken) {
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",accessToken);
        var response = await _client.GetAsync($"api/v1/coupon/{couponCode}");
        var content = await response.Content.ReadAsStringAsync();
        return response.StatusCode != HttpStatusCode.OK 
            ? new CouponVO() 
            : JsonSerializer.Deserialize<CouponVO>(content, new JsonSerializerOptions{ PropertyNameCaseInsensitive = true })!;
    }
}