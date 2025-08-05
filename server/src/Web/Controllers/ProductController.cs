using Application.DTOs;
using Application.Services.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;
public class ProductController : Controller
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }


    [Route("/products")]
    public IActionResult Index()
    {
        IEnumerable<ProductResponse> products = _productService.GetProducts();
        return View(products);
    }
}
