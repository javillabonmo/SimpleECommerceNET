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
    public IActionResult Index(string searchBy,string? searchString)
    {
        ViewBag.SearchFields = new Dictionary<string, string>()
        {
            {nameof(ProductResponse.Name),"Nombre" },
             {"Category"+nameof(ProductResponse.Category.Name),"Nombre Categoria" },
             {nameof(ProductResponse.Price),"Precio" },
             {nameof(ProductResponse.Stock),"Stock" },
             {nameof(ProductResponse.CreatedAt),"Creado El" },
             {nameof(ProductResponse.LastUpdatedAt),"Ultima Actualizacion" }
        };
        ViewBag.CurrentSearchBy = searchBy;
        ViewBag.CurrentSearchString = searchString;

        IEnumerable<ProductResponse> products = _productService.GetFilteredProducts(searchBy,searchString);
        return View(products);
    }

}
