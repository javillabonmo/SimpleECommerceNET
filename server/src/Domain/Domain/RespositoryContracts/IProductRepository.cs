namespace SimpleEcommerce.Core.Domain.RespositoryContracts
{
    using System.Linq.Expressions;

    using SimpleEcommerce.Core.Domain.Entities.Inventory;

    public interface IProductRepository
    {
        Task<Product> AddProduct(Product? product);

        Task<Product?> GetProductById(Guid id);

        Task<List<Product>> GetProducts();

        Task<Product?> UpdateProduct(Product? product);

        Task<bool> DeleteProduct(Guid id);

        Task<IEnumerable<Product>> GetFilteredProducts(Expression<Func<Product, bool>> predicate); // recibe una funcion lambda para filtrar

        Task<Product?> GetProductByName(string productName);
    }

}
