using Application.DTOs;

namespace Application.Services.Interfaces;

/*
 *
 * 1. validaciones de negocio y seguridad
 * 2. logica de validacion independiente del objeto
 * Name: No puede ser nulo o vacío
 * Price No puede ser menor o igual a cero
 * el Stock debe ser mayor o igual a cero
 * el Category debe existir
 * el producto no puede estar duplicado
 * validar que exista el producto antes de actualizar o eliminar
 *
 */
public interface IProductService
{
    ProductResponse AddProduct(RequestProduct? productAddRequest); //return the object created

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
    ProductResponse? UpdateProduct(int id,RequestProduct? productUpdateRequest); //return ProductDto con el objeto actualizado,true o null si no existe
    bool DeleteProduct(int id); //return true si se elimino, false si no existe
}