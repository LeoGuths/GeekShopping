﻿using GeekShopping.Web.Models;
using GeekShopping.Web.Services.IServices;
using GeekShopping.Web.Utils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.Web.Controllers;


public class ProductController : Controller {
    private readonly IProductService _productService;

    public ProductController(IProductService productService) {
        _productService = productService ?? throw new ArgumentNullException(nameof(productService));
    }

    [Authorize]
    public async Task<IActionResult> ProductIndex() {
        var token = await HttpContext.GetTokenAsync("access_token");
        var products = await _productService.FindAllProducts(token);
        return View(products);
    }
    
    public async Task<IActionResult> ProductCreate() {
        return View();
    }
    
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> ProductCreate(ProductViewModel viewModel) {
        if (ModelState.IsValid) {
            var token = await HttpContext.GetTokenAsync("access_token");
            var response = await _productService.CreateProduct(viewModel,token);
            if (response != null) return RedirectToAction(nameof(ProductIndex));
        }
        return View(viewModel);
    }
    
    public async Task<IActionResult> ProductUpdate(int id)
    {
        var token = await HttpContext.GetTokenAsync("access_token");
        var model = await _productService.FindProductById(id,token);
        if (model != null) return View(model);
        return NotFound();
    }
    
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> ProductUpdate(ProductViewModel viewModel) {
        if (ModelState.IsValid) {
            var token = await HttpContext.GetTokenAsync("access_token");
            var response = await _productService.UpdateProduct(viewModel,token);
            if (response != null) return RedirectToAction(nameof(ProductIndex));
        }
        return View(viewModel);
    }
    
    [Authorize]
    public async Task<IActionResult> ProductDelete(int id)
    {
        var token = await HttpContext.GetTokenAsync("access_token");
        var model = await _productService.FindProductById(id,token);
        if (model != null) return View(model);
        return NotFound();
    }
    
    [HttpPost]
    [Authorize(Roles = Role.Admin)]
    public async Task<IActionResult> ProductDelete(ProductViewModel viewModel) {
        var token = await HttpContext.GetTokenAsync("access_token");
        var response = await _productService.DeleteProductById(viewModel.Id,token);
        if (response) return RedirectToAction(nameof(ProductIndex));
        return View(viewModel);
    }
}