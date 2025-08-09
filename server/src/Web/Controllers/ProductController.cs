using Application.DTOs;
using Application.DTOs.Enums;
using Application.Services.Interfaces;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Controllers;
[Route("[controller]")]
public class ProductController : Controller
{
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;

    public ProductController(IProductService productService, ICategoryService categoryService)
    {
        _productService = productService;
        _categoryService = categoryService;
    }


    [Route("[action]")]
    public IActionResult Index(string searchBy,string? searchString,string sortBy = nameof(ProductResponse.ProductName),SortOrderEnum sortOrder = SortOrderEnum.Ascending)
    {
        ViewBag.SearchFields = new Dictionary<string, string>()
        {
            {nameof(ProductResponse.ProductName),"Nombre" },
             {"Category"+nameof(ProductResponse.Category.CategoryName),"Nombre Categoria" },
             {nameof(ProductResponse.Price),"Precio" },
             {nameof(ProductResponse.Stock),"Stock" },
             {nameof(ProductResponse.CreatedAt),"Creado El" },
             {nameof(ProductResponse.LastUpdatedAt),"Ultima Actualizacion" }
        };
        ViewBag.CurrentSearchBy = searchBy;
        ViewBag.CurrentSearchString = searchString;

        IEnumerable<ProductResponse> filteredProducts = _productService.GetFilteredProducts(searchBy,searchString);
        IEnumerable<ProductResponse> sortedProducts= _productService.GetSortedProducts(filteredProducts, sortBy,sortOrder);
        ViewBag.CurrentSortBy = sortBy;
        ViewBag.CurrentSortOrder = sortOrder.ToString();
        return View(sortedProducts);
    }

    [Route("[action]/{id}")]
    public IActionResult Detail(Guid id)
    {
        
        return View();
    }
    [Route("[action]")]
    [HttpGet]
    public IActionResult Create()
    {
        List<CategoryResponse> categories = _categoryService.GetCategories().ToList();
        ViewBag.Categories = categories.Select(category => new SelectListItem {Text = category.CategoryName, Value = category.Id.ToString() });
        return View();
    }
    [Route("[action]")]
    [HttpPost]
    public IActionResult Create(ProductRequest productAddRequest)
    {
        if (!ModelState.IsValid)
        {
            List<CategoryResponse> categories = _categoryService.GetCategories().ToList();
            ViewBag.Categories = categories.Select(category => new SelectListItem { Text = category.CategoryName, Value = category.Id.ToString() });
            ViewBag.Errors = ModelState.Values.SelectMany(value => value.Errors).Select(e => e.ErrorMessage).ToList();
            return View();
        }
        _productService.AddProduct(productAddRequest);
        return RedirectToAction("Index", "Product");
    }
    [Route("[action]/{id}")]
    [HttpGet]
    public IActionResult Update(Guid id)
    {
        ProductResponse? productResponse = _productService.GetProductById(id);
        if (productResponse ==null)
        {
            //NotFound();
            return RedirectToAction("Index","Product");
        }
        ProductUpdateRequest product =  productResponse.ToProductUpdateRequest();
        List<CategoryResponse> categories = _categoryService.GetCategories().ToList();
        ViewBag.Categories = categories.Select(category => new SelectListItem { Text = category.CategoryName, Value = category.Id.ToString() });
        return View(product);
    }
    
    [Route("[action]/{id}")]
    [HttpPost]
    public IActionResult Update(ProductUpdateRequest productUpdateRequest)
    {
        return View();
    }
    
    [Route("[action]/{id}")]
    public IActionResult Delete(Guid id)
    {
       
        return View();
    }
    
}
