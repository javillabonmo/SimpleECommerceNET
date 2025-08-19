
using Application.DTOs;
using Application.Services.Interfaces;

using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Controllers.FIlters.ActionFilters;

public class InvalidModelStateValidation : IAsyncActionFilter
{

    private readonly ICategoryService _categoryService;
    public InvalidModelStateValidation(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {


        if (context.Controller is ProductController productController)
        {
            if (!productController.ModelState.IsValid)
            {
                IEnumerable<CategoryResponse> categories = await _categoryService.GetCategories();
                productController.ViewBag.Categories = categories.Select(category => new SelectListItem { Text = category.CategoryName, Value = category.CategoryId.ToString() });
                productController.ViewBag.Errors = productController.ModelState.Values.SelectMany(value => value.Errors).Select(e => e.ErrorMessage).ToList();
                context.Result = productController.View(context.ActionArguments["productRequest"]); // result hace short circuit y no ejecuta ningun otro filtro o action
            }
            else
            {


                await next(); //adicionalmente si no se llama a next, tambien se interpreta el short circuit

            }
        }
    }
}
