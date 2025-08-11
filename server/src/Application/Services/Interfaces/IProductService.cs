


namespace Application.Services.Interfaces
{
    using Application.DTOs;
    using Application.DTOs.Enums;
    /*
 *
 * 1. validaciones de negocio y seguridad
 * 2. logica de validacion independiente del objeto
 * el Category debe existir
 * el producto no puede estar duplicado
 * validar que exista el producto antes de actualizar o eliminar
 *
 */
    public interface IProductService
    {
        ProductResponse AddProduct(ProductAddRequest? productAddRequest);

        // en lugar de usar null o false manejar excepciones para los casos de error


        ProductResponse? GetProductById(int id);

        ProductResponse? GetProductById(Guid id);

        IEnumerable<ProductResponse> GetProducts();

        ProductResponse? UpdateProduct(ProductUpdateRequest? productUpdateRequest); // return ProductDto con el objeto actualizado o null si no existe

        bool DeleteProduct(Guid id); // return true si se elimino, false si no existe

        // bool DeleteProduct(int id,Guid userId);//informacion del usuario que elimina el producto, puede ser un token de autenticacion 
        IEnumerable<ProductResponse> GetFilteredProducts(string searchBy, string? searchString);

        // IEnumerable<ProductResponse> GetFilteredProducts(string? searchBy, string? searchString, int pageNumber, int pageSize);

        // IEnumerable<ProductResponse> DeleteMultipleProductsById(IEnumerable<int> selectedIds);
        IEnumerable<ProductResponse> GetSortedProducts(IEnumerable<ProductResponse> products, string sortBy, SortOrderEnum sortOrder);
    }
}

