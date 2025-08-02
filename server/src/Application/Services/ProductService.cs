using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.DTOs;
using Application.Services.Helpers;
using Application.Services.Interfaces;
using Domain.Entities.Inventory;
using Domain.Entities.Sales;

namespace Application.Services;
public class ProductService : IProductService
//probando
{
    //dbcontext 
    private readonly List<Product> _products;
    private readonly ICategoryService _categories;

    // Constructor
    public ProductService()
    {
        // Simulando una base de datos en memoria
        _products = new List<Product>();
        _categories = new CategoryService(); 
    }

    public ProductResponse AddProduct(RequestProduct? productAddRequest)
    {
        if (productAddRequest == null)
        {
            throw new ArgumentNullException(nameof(productAddRequest), "RequestProduct cannot be null.");
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

        ValidationHelper.Validate(productAddRequest);

        // Convertir y agregar
        productAddRequest.Category = _categories.GetCategoryByGuid(productAddRequest.CategoryId);
        Product product = productAddRequest.ToProduct();
        product.Id = Guid.NewGuid();

        _products.Add(product);

        return product.ToProductResponse();
    }

    public bool DeleteProduct(int id)
    {
        throw new NotImplementedException();
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

    public ProductResponse? UpdateProduct(int id, RequestProduct? productUpdateRequest)
    {
        throw new NotImplementedException();
    }
}
