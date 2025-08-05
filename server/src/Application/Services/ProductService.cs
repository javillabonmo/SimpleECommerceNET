

using Application.DTOs;
using Application.DTOs.Enums;
using Application.Services.Helpers;

using Application.Services.Interfaces;
using Domain.Entities.Inventory;


namespace Application.Services;
using Mocks;

public class ProductService : IProductService
//probando
{
    //dbcontext 
    private readonly List<Product> _products;
    private readonly ICategoryService _categories;

    // Constructor
    public ProductService( bool initialize = true)
    {
        // Simulando una base de datos en memoria
        _products = new List<Product>();
        _categories = new CategoryService();
        if (initialize)
        {
            foreach (ProductResponse product in ProductMock.All())
            {
                // Convertir ProductResponse a Product y agregarlo a la lista
                _products.Add(product.Mock());
            }
        }
    }

    

    public ProductResponse AddProduct(ProductRequest? productAddRequest)
    {
        if (productAddRequest == null)
        {
            throw new ArgumentNullException(nameof(productAddRequest), "ProductRequest cannot be null.");
        }

        if (string.IsNullOrWhiteSpace(productAddRequest.Name))
        {
            throw new ArgumentException("Product name cannot be null or empty.", nameof(productAddRequest.Name));
        }
        if (productAddRequest.Price <= 0)
        {
            throw new ArgumentException("Product price must be greater than zero.", nameof(productAddRequest.Price));
        }

        // Validar duplicados antes de agregar
        if (_products.Any(p => p.Name == productAddRequest.Name))
        {
            throw new ArgumentException("Product with the same name already exists.", nameof(productAddRequest.Name));
        }
        if (productAddRequest.CategoryId == Guid.Empty)
        {
            throw new ArgumentException("CategoryId cannot be null.", nameof(productAddRequest.CategoryId));
        }

        ValidationHelper.Validate(productAddRequest);

        // Convertir y agregar
        
        /*var category = _categories.GetCategoryByGuid(guid: productAddRequest.CategoryId);
        if (category == null)
        {
            throw new ArgumentException("Category not found for the given CategoryId.", nameof(productAddRequest.CategoryId));
        }*/
        productAddRequest.Category = _categories.GetCategoryByGuid(guid: productAddRequest.CategoryId);
        Product product = productAddRequest.ToProduct();
        product.Id = Guid.NewGuid();

        _products.Add(product);

        return product.ToProductResponse();
    }

    public bool DeleteProduct(Guid id)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentException("Product ID cannot be empty.", nameof(id));
        }
        // Check if the product exists
        Product? productToDelete = _products.FirstOrDefault(p => p.Id == id);
        if (productToDelete == null)
        {
            return false; // Product not found, nothing to delete
        }
        _products.Remove(productToDelete);
        return true; // Product successfully deleted
    }

    public IEnumerable<ProductResponse> GetFilteredProducts(string searchBy, string? searchString)
    {
       IEnumerable<ProductResponse> allProducts = GetProducts();
        if (string.IsNullOrWhiteSpace(searchString))
        {
            return allProducts;
        }
        return searchBy.ToLower() switch
        {   
            nameof(Product.Name) => allProducts.Where(p => p.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase)),
            nameof(Product.Category) =>allProducts.Where(p => p.Category.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase)),
            _ => throw new ArgumentException("Invalid search criteria.", nameof(searchBy))
            //para fechas se convierte en string y se compara con Contains
        };
    }

    public ProductResponse? GetProductById(int id)
    {
        throw new NotImplementedException();
    }

    public ProductResponse? GetProductById(Guid id)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentException("Product ID cannot be empty.", nameof(id));
        }
        //si retorno null es por que no es un error que no haya encontrado el producto, por lo tanto no tiene sentido lanzar una excepcion
        return _products
            .FirstOrDefault(product => product.Id == id)?
            .ToProductResponse();//operador condicional null (?.) para evitar excepciones si el producto no se encuentra
    }

    public IEnumerable<ProductResponse> GetProducts()
    {
        //throw new NotImplementedException();
        return _products.Select(product => product.ToProductResponse());
    }

    public IEnumerable<ProductResponse> GetSortedProducts(IEnumerable<ProductResponse> products, string sortBy, SortOrderEnum sortOrder)
    {
        if (products == null || !products.Any())
        {
            return Enumerable.Empty<ProductResponse>();
        }
        if (string.IsNullOrWhiteSpace(sortBy))
        {
            return products; // No sorting applied
        }

        return sortBy switch
        {
            nameof(Product.Name) => sortOrder == SortOrderEnum.Ascending
                ? products.OrderBy(p => p.Name)
                : products.OrderByDescending(p => p.Name),
            nameof(Product.Price) => sortOrder == SortOrderEnum.Ascending
                ? products.OrderBy(p => p.Price)
                : products.OrderByDescending(p => p.Price),
            nameof(Product.Stock) => sortOrder == SortOrderEnum.Ascending
                ? products.OrderBy(p => p.Stock)
                : products.OrderByDescending(p => p.Stock),
            _ => throw new ArgumentException("Invalid sort criteria.", nameof(sortBy))
        };
    }

    public ProductResponse? UpdateProduct(ProductUpdateRequest? productUpdateRequest)
    {
        if (productUpdateRequest == null) {
            return null; // or throw an exception if you prefer
        }
        ValidationHelper.Validate(productUpdateRequest);
        if (productUpdateRequest.Id == Guid.Empty)
        {
            throw new ArgumentException("Product ID cannot be empty.", nameof(productUpdateRequest.Id));
        }
        // check if the product exists
        Product? existingProduct = _products.FirstOrDefault(p => p.Id == productUpdateRequest.Id);
        if (existingProduct == null)
        {
            throw new KeyNotFoundException($"Product with ID {productUpdateRequest.Id} not found.");
        }

        ValidationHelper.Validate(productUpdateRequest);

        ProductUpdateRequest productToUpdate = productUpdateRequest;

        // Update the product properties
        productToUpdate.Name = productUpdateRequest.Name ?? existingProduct.Name;

        productToUpdate.Price = productUpdateRequest.Price > 0 ? productUpdateRequest.Price : existingProduct.Price;

        productToUpdate.Stock = productUpdateRequest.Stock >= 0 ? productUpdateRequest.Stock : existingProduct.Stock;
        //productToUpdate.Category = productUpdateRequest.Category ?? existingProduct.Category.ToCategoryResponse();
        // Convert the updated request back to a Product entity
        Product updatedProduct = productToUpdate.ToProduct();

        return updatedProduct.ToProductResponse();
    }
}
