using GeekShopping.ProductAPI.Data.ValueObjects;
using GeekShopping.ProductAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.ProductAPI.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private IProductRepository _repository;

    public ProductController(IProductRepository repository)
    {
        _repository = repository ?? throw new ArgumentException(null, nameof(repository));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductVO>>> FindAll()
    {
        IEnumerable<ProductVO> products = await _repository.FindAll();
        return Ok(products);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<ProductVO>> FindById(long id)
    {
        ProductVO product = await _repository.FindById(id);
        if (product == null) return NotFound();
        return Ok(product);
    }
    
    [HttpPost]
    public async Task<ActionResult<ProductVO>> Create([FromBody]ProductVO vo)
    {
        if (vo == null) return BadRequest();
        ProductVO product = await _repository.Create(vo);
        return Ok(product);
    }
    
    [HttpPut]
    public async Task<ActionResult<ProductVO>> Update([FromBody]ProductVO vo)
    {
        if (vo == null) return BadRequest();
        ProductVO product = await _repository.Update(vo);
        return Ok(product);
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(long id)
    {
        bool status = await _repository.Delete(id);
        if (!status) return BadRequest();
        return Ok(status);
    }
}