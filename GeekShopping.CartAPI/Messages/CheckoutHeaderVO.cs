using GeekShopping.CartAPI.Data.ValueObjects;

namespace GeekShopping.CartAPI.Messages; 

public class CheckoutHeaderVO {
    public long Id { get; set; }
    public string? UserId { get; set; } = "";
    public string? CouponCode { get; set; } = "";
    public decimal PurchaseAmout { get; set; }
    public decimal DiscountAmount { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime Time { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string CardNumber { get; set; }
    public string CardCvv { get; set; }
    public string CardExpiryMonthYear { get; set; }
    public int CartTotalItems { get; set; }
    public List<CartDetailVO>? CartDetails { get; set; }
}