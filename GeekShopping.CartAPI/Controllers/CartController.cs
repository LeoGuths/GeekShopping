using GeekShopping.CartAPI.Data.ValueObjects;
using GeekShopping.CartAPI.Messages;
using GeekShopping.CartAPI.RabbitMqSender;
using GeekShopping.CartAPI.Repository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.CartAPI.Controllers; 

[ApiController]
[Route("api/v1/[controller]")]
public class CartController : ControllerBase {
    private ICartRepository _cartRepository;
    private ICouponRepository _couponRepository;
    private IRabbitMqMessageSender _rabbitMqMessageSender;

    public CartController(ICartRepository cartRepository, ICouponRepository couponRepository, IRabbitMqMessageSender rabbitMqMessageSender) {
        _cartRepository = cartRepository ?? throw new ArgumentNullException(nameof(cartRepository));
        _couponRepository = couponRepository ?? throw new ArgumentNullException(nameof(couponRepository));
        _rabbitMqMessageSender = rabbitMqMessageSender ?? throw new ArgumentNullException(nameof(rabbitMqMessageSender));
    }

    [HttpGet("find-cart/{id}")]
    [Authorize]
    public async Task<ActionResult<CartVO>> FindById(string id)
    {
        CartVO cart = await _cartRepository.FindCartByUserId(id);
        if (cart == null) return NotFound();
        return Ok(cart);
    }
    
    [HttpPost("add-cart")]
    public async Task<ActionResult<CartVO>> AddCart(CartVO vo) {
        CartVO cart = await _cartRepository.SaveOrUpdateCart(vo);
        if (cart == null) return NotFound();
        return Ok(cart);
    }
    
    [HttpPut("update-cart")]
    public async Task<ActionResult<CartVO>> UpdateCart(CartVO vo) {
        CartVO cart = await _cartRepository.SaveOrUpdateCart(vo);
        if (cart == null) return NotFound();
        return Ok(cart);
    }
    
    [HttpDelete("remove-cart/{id}")]
    public async Task<ActionResult<CartVO>> RemoveCart(int id) {
        bool status = await _cartRepository.RemoveFromCart(id);
        if (!status) return BadRequest();
        return Ok(status);
    }
    
    [HttpPost("apply-coupon")]
    public async Task<ActionResult<CartVO>> ApplyCoupon(CartVO vo) {
        bool status = await _cartRepository.ApplyCoupon(vo.CartHeader.UserId, vo.CartHeader.CouponCode);
        if (!status) return NotFound();
        return Ok(status);
    }
    
    [HttpDelete("remove-coupon/{userId}")]
    public async Task<ActionResult<CartVO>> RemoveCoupon(string userId) {
        bool status = await _cartRepository.RemoveCoupon(userId);
        if (!status) return NotFound();
        return Ok(status);
    }
    
    [HttpPost("checkout")]
    public async Task<ActionResult<CheckoutHeaderVO>> Checkout(CheckoutHeaderVO vo) {
        if (vo?.UserId == null) return BadRequest();
        var cart = await _cartRepository.FindCartByUserId(vo.UserId);
        if (cart == null) return NotFound();
        if (!string.IsNullOrEmpty(vo.CouponCode)) {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var coupon = await _couponRepository.GetCoupon(vo.CouponCode, accessToken);
            if (vo.DiscountAmount != coupon.DiscountAmount) {
                return StatusCode(412);
            }
        }
        vo.CartDetails = cart.CartDetails;
        vo.Time = DateTime.UtcNow;
        
        _rabbitMqMessageSender.SendMessage(vo,"checkoutQueue");
        await _cartRepository.ClearCart(vo.UserId);
        
        return Ok(vo);
    }
}