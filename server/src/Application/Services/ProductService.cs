


namespace Application.Services
{
    using Application.DTOs;
    using Application.DTOs.Enums;
    using Application.Services.Helpers;

    using Application.Services.Interfaces;

    using Domain.Entities.Inventory;
    using Domain.Entities.Sales;

    using Mocks;
    using Infraestructure.Persistence;
    using Microsoft.EntityFrameworkCore;

    public class ProductService : IProductService
    //probando
    {
        private readonly ApplicationDbContext _context;
        //private readonly List<Product> _products;
        // private readonly ICategoryService _categories;


        public ProductService(ApplicationDbContext applicationDbContext, bool initialize = false)
        {

            //_products = new List<Product>();
            // _categories = new CategoryService();
            if (initialize)
            {
                foreach (Product product in ProductMock.All())
                {
                    // Convertir ProductResponse a Product y agregarlo a la lista
                    //_products.Add(product);
                }
            }

            _context = applicationDbContext;
        }



        public ProductResponse AddProduct(ProductAddRequest? productAddRequest)
        {
            /*
            if (productAddRequest == null)
            {
                throw new ArgumentNullException(nameof(productAddRequest), "ProductAddRequest cannot be null.");
            }

            if (string.IsNullOrWhiteSpace(productAddRequest.ProductName))
            {
                throw new ArgumentException("Product name cannot be null or empty.", nameof(productAddRequest.ProductName));
            }
            */
            /*
            if (productAddRequest.Price <= 0)
            {
                throw new ArgumentException("Product price must be greater than zero.", nameof(productAddRequest.Price));
            }
            */

            // Validar duplicados antes de agregar
            if (_context.Products.Any(p => p.ProductName == productAddRequest.ProductName))
            {
                throw new ArgumentException("Product with the same name already exists.", nameof(productAddRequest.ProductName));
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


            Product product = productAddRequest.ToProduct();
            product.ProductId = Guid.NewGuid();

            _context.Add(product);
            _context.SaveChanges();
            return product.ToProductResponse();
        }

        public bool DeleteProduct(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Product ID cannot be empty.", nameof(id));
            }
            // Check if the product exists
            Product? product = _context.Products.FirstOrDefault(p => p.ProductId == id);
            if (product == null)
            {
                return false; // Product not found, nothing to delete
            }
            _context.Products.Remove(_context.Products.First(prod => prod.ProductId == id));
            _context.SaveChanges();
            return true; // Product successfully deleted
        }

        public IEnumerable<ProductResponse> GetFilteredProducts(string searchBy, string? searchString)
        {
            IEnumerable<ProductResponse> allProducts = GetProducts();
            if (string.IsNullOrWhiteSpace(searchString) || String.IsNullOrEmpty(searchBy))
            {
                return allProducts;
            }

            return searchBy switch
            {
                nameof(Product.ProductName) => allProducts.Where(p => p.ProductName.Contains(searchString, StringComparison.OrdinalIgnoreCase)),
                nameof(Product.Category) => allProducts.Where(p => p.Category.CategoryName.Contains(searchString, StringComparison.OrdinalIgnoreCase)),
                nameof(Product.Stock) =>
                    uint.TryParse(searchString, out uint stockValue)
                        ? allProducts.Where(p => p.Stock >= stockValue)
                        : Enumerable.Empty<ProductResponse>(),
                nameof(Product.Price) => decimal.TryParse(searchString, out decimal searchPrice)
                        ? allProducts.Where(p => p.Price >= searchPrice)
                        : Enumerable.Empty<ProductResponse>(),
                _ => throw new ArgumentException("Invalid search criteria.", nameof(searchBy))
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
            return _context.Products
                .FirstOrDefault(product => product.ProductId == id)?
                .ToProductResponse();//operador condicional null (?.) para evitar excepciones si el producto no se encuentra
        }

        public IEnumerable<ProductResponse> GetProducts()
        {
            //throw new NotImplementedException();
            return _context.Products.Include("Category")
                .Select(product => product.ToProductResponse());
            //return _context.Products.Select(product => product.ToProductResponse());
        }

        public IEnumerable<ProductResponse> GetSortedProducts(IEnumerable<ProductResponse> products, string sortBy, SortOrderEnum sortOrder)
        {
            /*
            if (products == null || !products.Any())
            {
                return Enumerable.Empty<ProductResponse>();
            }

            if (string.IsNullOrWhiteSpace(sortBy))
            {
                return products; // No sorting applied
            }
            */

            return sortBy switch
            {
                nameof(Product.ProductName) => sortOrder == SortOrderEnum.Ascending
                    ? products.OrderBy(p => p.ProductName)
                    : products.OrderByDescending(p => p.ProductName),
                nameof(Product.Price) => sortOrder == SortOrderEnum.Ascending
                    ? products.OrderBy(p => p.Price)
                    : products.OrderByDescending(p => p.Price),
                nameof(Product.Stock) => sortOrder == SortOrderEnum.Ascending
                    ? products.OrderBy(p => p.Stock)
                    : products.OrderByDescending(p => p.Stock),
                nameof(Product.Category.CategoryName) => sortOrder == SortOrderEnum.Ascending ? products.OrderBy(p => p.Category.CategoryName) : products.OrderByDescending(p => p.Category.CategoryName),
                _ => products
            };
        }

        public ProductResponse? UpdateProduct(ProductUpdateRequest? productUpdateRequest)
        {
            if (productUpdateRequest == null)
            {
                return null; // or throw an exception if you prefer
            }

            if (productUpdateRequest.ProductId == Guid.Empty)
            {
                throw new ArgumentException("Product ID cannot be empty.", nameof(productUpdateRequest.ProductId));
            }
            // check if the product exists
            Product? existingProduct = _context.Products.FirstOrDefault(p => p.ProductId == productUpdateRequest.ProductId);
            if (existingProduct == null)
            {
                throw new KeyNotFoundException($"Product with ID {productUpdateRequest.ProductId} not found.");
            }

            ValidationHelper.Validate(productUpdateRequest);



            // Update the product properties
            existingProduct.ProductName = productUpdateRequest.ProductName ?? existingProduct.ProductName;

            existingProduct.Price = productUpdateRequest.Price > 0 ? productUpdateRequest.Price : existingProduct.Price;

            existingProduct.Stock = productUpdateRequest.Stock >= 0 ? productUpdateRequest.Stock : existingProduct.Stock;
            existingProduct.Category = productUpdateRequest.Category ?? existingProduct.Category;

            _context.SaveChanges();


            return existingProduct.ToProductResponse();
        }
    }



}


