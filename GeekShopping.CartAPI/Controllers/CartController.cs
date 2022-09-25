using GeekShopping.CartAPI.Data.ValueObjects;
using GeekShopping.CartAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.CartAPI.Controllers; 

[ApiController]
[Route("api/v1/[controller]")]
public class CartController : ControllerBase {
    private ICartRepository _repository;

    public CartController(ICartRepository repository)
    {
        _repository = repository ?? throw new ArgumentException(null, nameof(repository));
    }

    [HttpGet("find-cart/{id}")]
    //Authorize]
    public async Task<ActionResult<CartVO>> FindById(string userId)
    {
        CartVO cart = await _repository.FindCartByUserId(userId);
        if (cart == null) return NotFound();
        return Ok(cart);
    }
    
    [HttpPost("add-cart/{id}")]
    //Authorize]
    public async Task<ActionResult<CartVO>> AddCart(CartVO vo) {
        CartVO cart = await _repository.SaveOrUpdateCart(vo);
        if (cart == null) return NotFound();
        return Ok(cart);
    }
    
    [HttpPut("update-cart/{id}")]
    //Authorize]
    public async Task<ActionResult<CartVO>> UpdateCart(CartVO vo) {
        CartVO cart = await _repository.SaveOrUpdateCart(vo);
        if (cart == null) return NotFound();
        return Ok(cart);
    }
    
    [HttpDelete("remove-cart/{id}")]
    //Authorize]
    public async Task<ActionResult<CartVO>> RemoveCart(int id) {
        bool status = await _repository.RemoveFromCart(id);
        if (!status) return BadRequest();
        return Ok(status);
    }
}