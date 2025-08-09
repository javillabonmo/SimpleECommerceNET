using Application.DTOs;
using Application.DTOs.Enums;


namespace Application.Services.Interfaces;

/*
 *
 * 1. validaciones de negocio y seguridad
 * 2. logica de validacion independiente del objeto
 * ProductName: No puede ser nulo o vacío
 * Price No puede ser menor o igual a cero
 * el Stock debe ser mayor o igual a cero
 * el Category debe existir
 * el producto no puede estar duplicado
 * validar que exista el producto antes de actualizar o eliminar
 *
 */
public interface IProductService
{
    ProductResponse AddProduct(ProductRequest? productAddRequest); //return the object created

    //en lugar de usar null o false manejar excepciones para los casos de error
    //GetProductById |get| -> return the object o null si no existe
    // GetProducts |get| -> return all objects o null si no existe, 
    // UpdateProduct |put-patch| -> return ProductDto con el objeto actualizado,true o null si no existe
    // DeleteProduct |delete| -> return true si se elimino, false si no existe

    ProductResponse? GetProductById(int id);
    ///<summary>
    /// Retorna un producto por su ID.
    ///</summary>
    ///<param name="id">guid por el cual buscar el producto</param>
    ///<returns>retorna un objeto producto</returns>
    ProductResponse? GetProductById(Guid id);
    IEnumerable<ProductResponse> GetProducts(); 
    ProductResponse? UpdateProduct(ProductUpdateRequest? productUpdateRequest); //return ProductDto con el objeto actualizado,true o null si no existe
    bool DeleteProduct(Guid id); //return true si se elimino, false si no existe
    // bool DeleteProduct(int id,Guid userId);//informacion del usuario que elimina el producto, puede ser un token de autenticacion 
    IEnumerable<ProductResponse> GetFilteredProducts(string searchBy,string? searchString);

    //IEnumerable<ProductResponse> GetFilteredProducts(string? searchBy, string? searchString, int pageNumber, int pageSize);

    //IEnumerable<ProductResponse> DeleteMultipleProductsById(IEnumerable<int> selectedIds);


    IEnumerable<ProductResponse> GetSortedProducts(IEnumerable<ProductResponse> products,string sortBy, SortOrderEnum sortOrder);
}