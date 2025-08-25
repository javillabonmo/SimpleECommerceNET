namespace SimepleECommerce.Infraestructure.Repositories
{
    using System.Linq.Expressions;


    using SimpleECommerce.Infraestructure.Persistence;

    using Microsoft.EntityFrameworkCore;

    using SimpleEcommerce.Core.Domain.Entities.Inventory;
    using SimpleEcommerce.Core.Domain.RespositoryContracts;

    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Product> AddProduct(Product? product)
        {
            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync();
            return product;
        }

        public async Task<bool> DeleteProduct(Guid id)
        {
            _dbContext.RemoveRange(_dbContext.Products.Where(p => p.ProductId == id));
            int deletedRows = await _dbContext.SaveChangesAsync();
            return deletedRows > 0;
        }

        public async Task<IEnumerable<Product>> GetFilteredProducts(Expression<Func<Product, bool>> predicate)
        {
            return await _dbContext.Products.Include("Category").Where(predicate).ToListAsync();
        }

        public async Task<Product?> GetProductById(Guid id)
        {
            Product? Product = await _dbContext.Products.Include("Category").FirstOrDefaultAsync(p => p.ProductId == id);
            return Product;
        }

        public async Task<Product?> GetProductByName(string productName)
        {
            Product? product = await _dbContext.Products.Where(p => p.ProductName == productName).FirstOrDefaultAsync();
            return product;
        }

        public async Task<List<Product>> GetProducts()
        {
            return await _dbContext.Products.Include("Category").ToListAsync();
        }

        public async Task<Product?> UpdateProduct(Product? product)
        {
            Product? existingProduct = await _dbContext.Products.FirstOrDefaultAsync(p => p.ProductId == product.ProductId);
            if (existingProduct == null) return null;

            existingProduct.ProductName = product.ProductName ?? existingProduct.ProductName;

            existingProduct.Price = product.Price > 0 ? product.Price : existingProduct.Price;

            existingProduct.Stock = product.Stock >= 0 ? product.Stock : existingProduct.Stock;
            existingProduct.Category = product.Category ?? existingProduct.Category;

            await _dbContext.SaveChangesAsync();

            return existingProduct;
        }
    }

}
