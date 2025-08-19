

namespace Web.Controllers.FIlters.ActionFilters
{
    using System.Threading.Tasks;

    using Application.DTOs;

    using Microsoft.AspNetCore.Mvc.Filters;

    public class ProductActionFilter : IAsyncActionFilter
    {
        private readonly ILogger<ProductActionFilter> _logger;

        public ProductActionFilter(ILogger<ProductActionFilter> logger)
        {
            // Constructor logic if needed
            _logger = logger;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // OnActionExecuting
            context.HttpContext.Items["Arguments"] = context.ActionArguments;

            if (context.ActionArguments.ContainsKey("searchBy"))
            {
                // 1. Validar si SearchBy posee los valores del modelo ProductResponse
                var searchBy = context.ActionArguments["searchBy"] as string;
                if (string.IsNullOrEmpty(searchBy))
                {
                    string[] validSearchFields = { "ProductName", "Category.CategoryName", "Price", "Stock", "CreatedAt", "LastUpdatedAt" };
                    if (!validSearchFields.Contains(searchBy))
                    {
                        _logger.LogError($"Invalid searchBy value: {searchBy}. Valid values are: {string.Join(", ", validSearchFields)}");
                        context.ModelState.AddModelError("searchBy", $"El campo 'searchBy' debe ser uno de los siguientes: {string.Join(", ", validSearchFields)}");
                        context.ActionArguments["searchBy"] = nameof(ProductResponse.ProductName); // Set to null to prevent further processing
                    }
                }
            }

            await next();

            // OnActionExecuted
            ProductController productController = (ProductController)context.Controller;

            IDictionary<string, object?> actionArguments = (IDictionary<string, object?>)context.HttpContext.Items["Arguments"];

            if (actionArguments.ContainsKey("searchBy"))
            {
                productController.ViewData["searchBy"] = actionArguments["searchBy"];
            }
        }
    }
}

