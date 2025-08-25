

namespace Web.Controllers
{


    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;

    using SimpleEcommerce.Core.DTOs;

    using SimpleECommerce.Core.DTOs;
    using SimpleECommerce.Core.Enums;
    using SimpleECommerce.Core.ServicesContrats;

    using Web.Controllers.FIlters.ActionFilters;
    using Web.Controllers.FIlters.AuthorizationFilters;
    using Web.Controllers.FIlters.ResultFilters;

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
        public async Task<IActionResult> Index(string searchBy, string? searchString, string sortBy = nameof(ProductResponse.ProductName), SortOrderEnum sortOrder = SortOrderEnum.Ascending)
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

            IEnumerable<ProductResponse> filteredProducts = await _productService.GetFilteredProducts(searchBy, searchString);
            IEnumerable<ProductResponse> sortedProducts = _productService.GetSortedProducts(filteredProducts, sortBy, sortOrder);
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
        public async Task<IActionResult> Create()
        {
            IEnumerable<CategoryResponse> categories = await _categoryService.GetCategories();
            ViewBag.Categories = categories.Select(category => new SelectListItem { Text = category.CategoryName, Value = category.CategoryId.ToString() });
            return View();
        }
        [Route("[action]")]
        [HttpPost]

        public async Task<IActionResult> Create(ProductRequest productRequest)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<CategoryResponse> categories = await _categoryService.GetCategories();
                ViewBag.Categories = categories.Select(category => new SelectListItem { Text = category.CategoryName, Value = category.CategoryId.ToString() });
                ViewBag.Errors = ModelState.Values.SelectMany(value => value.Errors).Select(e => e.ErrorMessage).ToList();
                return View();
            }
            await _productService.AddProduct(productRequest);
            return RedirectToAction("Index", "Product");
        }
        [Route("[action]/{id}")]
        [HttpGet]
        [TypeFilter(typeof(TokenResultFilter))]
        public async Task<IActionResult> Update(Guid id)
        {
            ProductResponse? productResponse = await _productService.GetProductById(id);
            if (productResponse == null)
            {
                //NotFound();
                return RedirectToAction("Index", "Product");
            }
            ProductUpdateRequest product = productResponse.ToProductUpdateRequest();
            IEnumerable<CategoryResponse> categories = await _categoryService.GetCategories();
            ViewBag.Categories = categories.Select(category => new SelectListItem { Text = category.CategoryName, Value = category.CategoryId.ToString() });
            return View(product);
        }

        [Route("[action]/{id}")]
        [HttpPost]
        [TypeFilter(typeof(InvalidModelStateValidation))]
        [TypeFilter(typeof(TokenAuthorizationFIlter))]
        public async Task<IActionResult> Update(ProductUpdateRequest productRequest)
        {
            ProductResponse? productResponse = await _productService.GetProductById(productRequest.ProductId);
            if (productResponse == null)
            {
                //NotFound();
                return RedirectToAction("Index", "Product");
            }

            await _productService.UpdateProduct(productRequest);
            return RedirectToAction("Index", "Product");


        }

        [Route("[action]/{id}")]
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {

            ProductResponse? productResponse = await _productService.GetProductById(id);
            if (productResponse == null)
            {
                return RedirectToAction("Index", "Product");
            }

            return View(productResponse);
        }
        [Route("[action]/{id}")]
        [HttpPost]
        public async Task<IActionResult> Delete(ProductUpdateRequest productUpdateRequest)
        {
            ProductResponse? productResponse = await _productService.GetProductById(productUpdateRequest.ProductId);
            if (productResponse == null)
            {
                //NotFound();
                return RedirectToAction("Index", "Product");
            }

            await _productService.DeleteProduct(productUpdateRequest.ProductId);
            return RedirectToAction("Index", "Product");

        }

    }

}
