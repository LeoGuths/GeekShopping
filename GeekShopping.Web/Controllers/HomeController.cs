using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using GeekShopping.Web.Models;
using GeekShopping.Web.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace GeekShopping.Web.Controllers;

public class HomeController : Controller
{
    private readonly IProductService _productService;
    private readonly ICartService _cartService;
    private readonly ILogger<HomeController> _logger;

    public HomeController(IProductService productService, ICartService cartService, ILogger<HomeController> logger)
    {
        _productService = productService;
        _cartService = cartService;
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        var products = await _productService.FindAllProducts("");
        return View(products);
    }
    
    [Authorize]
    public async Task<IActionResult> Details(int id)
    {
        var accessToken = await HttpContext.GetTokenAsync("access_token");
        var model = await _productService.FindProductById(id, accessToken!);
        return View(model);
    }
    
    [HttpPost]
    [ActionName("Details")]
    [Authorize]
    public async Task<IActionResult> DetailsPost(ProductViewModel model)
    {
        var accessToken = await HttpContext.GetTokenAsync("access_token");
        var cart = new CartViewModel {
            CartHeader = new CartHeaderViewModel {
                UserId = User.Claims.Where(c => c.Type == "sub").FirstOrDefault()?.Value
            }
        };
        var cartDetail = new CartDetailViewModel {
            Count = model.Count,
            ProductId = model.Id,
            Product = await _productService.FindProductById(model.Id, accessToken)
        };
        var cartDetails = new List<CartDetailViewModel> { cartDetail };
        cart.CartDetails = cartDetails;

        var response = await _cartService.AddItemToCart(cart, accessToken);
        if (response != null) {
            return RedirectToAction(nameof(Index));
        }
        return View(model);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    
    [Authorize]
    public async Task<IActionResult> Login()
    {
        var accessToken = await HttpContext.GetTokenAsync("access_token");
        return RedirectToAction(nameof(Index));
    }
    
    public IActionResult Logout()
    {
        return SignOut("Cookies", "oidc");
    }
}