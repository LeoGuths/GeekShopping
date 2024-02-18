using GeekShopping.ProductAPI.Data.ValueObjects;
using GeekShopping.ProductAPI.Model;
using GeekShopping.ProductAPI.Model.Context;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.ProductAPI.Repository;

public class ProductRepository : IProductRepository
{
    private readonly MySqlContext _context;

    public ProductRepository(MySqlContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<ProductVO>> FindAll()
    {
        var products = await _context.Products.ToListAsync();
        return products.Adapt<List<ProductVO>>();
    }

    public async Task<ProductVO> FindById(long id)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id) ?? new Product();
        return product.Adapt<ProductVO>();
    }

    public async Task<ProductVO> Create(ProductVO vo)
    {
        var product = vo.Adapt<Product>();
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return product.Adapt<ProductVO>();
    }

    public async Task<ProductVO> Update(ProductVO vo)
    {
        var product = vo.Adapt<Product>();
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
        return product.Adapt<ProductVO>();
    }

    public async Task<bool> Delete(long id)
    {
        try
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id) ?? new Product();
            if (product.Id <= 0) return false;
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}