using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.DTOs;
using Application.Services.Interfaces;
using Domain.Entities.Inventory;

namespace Application.Services;
public class ProductService : IProductService
//probando
{
    //dbcontext 
    private readonly List<Product> _products;

    // Constructor
    public ProductService()
    {
        // Simulando una base de datos en memoria
        _products = new List<Product>();
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

        // Convertir y agregar
        Product product = productAddRequest.ToProduct();
        product.Id = Guid.NewGuid();
        _products.Add(product);

        return product.ToResponse();
    }

    public bool DeleteProduct(int id)
    {
        throw new NotImplementedException();
    }

    public ProductResponse? GetProductById(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<ProductResponse> GetProducts()
    {
        throw new NotImplementedException();
    }

    public ProductResponse? UpdateProduct(int id, ProductRequest? productUpdateRequest)
    {
        throw new NotImplementedException();
    }
}
