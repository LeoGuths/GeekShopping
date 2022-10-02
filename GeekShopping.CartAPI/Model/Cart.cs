namespace GeekShopping.CartAPI.Model;

public class Cart {
    public CartHeader? CartHeader { get; set; } = new();
    public List<CartDetail>? CartDetails { get; set; } = new();
}