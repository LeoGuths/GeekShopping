using GeekShopping.CouponAPI.Data.ValueObjects;
using GeekShopping.CouponAPI.Model.Context;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.CouponAPI.Repository; 

public class CouponRepository : ICouponRepository {
    private readonly MySqlContext _context;

    public CouponRepository(MySqlContext context) {
        _context = context;
    }
    
    public async Task<CouponVO> GetCouponByCouponCode(string couponCode) {
        var coupon = await _context.Coupons.FirstOrDefaultAsync(c => c.CouponCode == couponCode);
        return coupon.Adapt<CouponVO>();
    }
}