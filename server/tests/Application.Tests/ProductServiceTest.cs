

using Application.DTOs;

namespace Application.Tests;
using Application.Services.Interfaces;
using Application.Services;
public class ProductServiceTest
{
    /*
     * Asegura que las reglas de negocio y seguridad se cumplan
     * verificar que se lancen las excepciones correctas
     * 1.Cuando RequestProduct.Name es null, se lanza ArgumentException
     * 2.Cuando el Price es 0, lanza excepción
     * Cuando el producto se guarda bien, su Id no debe ser Guid.Empty
     * Cuando se consulta por ID inexistente, devuelve null o lanza NotFoundException
     *
     * El objetivo es: validar que el servicio se comporte como debe bajo distintos escenarios
     * los test deben fallar si la lógica de negocio falla
     *
     * las validaciones van en el servicio y se comprueban en los test
     */

    private readonly IProductService _productService;
    public  ProductServiceTest()
    {
        _productService =  new ProductService(); 
    }

    #region AddProduct


    //cada requerimiento es una funcion de test
    //MétodoAEvaluar_CondiciónEsperada_ResultadoEsperado

    //2. si RequestProduct es nulo, debe lanzar una excepción
    [Fact]
    public void AddProduct_NullRequest_ThrowsArgumentNullException()
    {
        // Arrange
        RequestProduct? productAddRequest = null;
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => _productService.AddProduct(productAddRequest));
    }
    //3. si Name es nulo o vacío, debe lanzar una excepción
    [Fact]
    public void AddProduct_NullOrEmptyName_ThrowsArgumentException()
    {
        // Arrange
        RequestProduct productAddRequest = new RequestProduct
        {
            Name = null, // or string.Empty
            Price = 10.0m,
       
        };
        // Act & Assert
        Assert.Throws<ArgumentException>(() => _productService.AddProduct(productAddRequest));
    }
    //4. si Price es menor o igual a cero, debe lanzar una excepción
    [Fact]
    public void AddProduct_NegativeOrZeroPrice_ThrowsArgumentException()
    {
        // Arrange
        RequestProduct productAddRequest = new RequestProduct
        {
            Name = "Test Product",
            Price = 0.0m, // or negative value
        };
        // Act & Assert
        Assert.Throws<ArgumentException>(() => _productService.AddProduct(productAddRequest));
    }
    //5. si Name esta duplicado, debe lanzar una excepción
    [Fact]
    public void AddProduct_DuplicateName_ArgumentExceptionn()
    {
        // Arrange
        RequestProduct productAddRequest1 = new RequestProduct
        {
            Name = "Test Product",
            Price = 10.0m,
        };
        _productService.AddProduct(productAddRequest1); // Add first product
        RequestProduct productAddRequest2 = new RequestProduct
        {
            Name = "Test Product", // Duplicate name
            Price = 15.0m,
        };
        // Act & Assert
        Assert.Throws<ArgumentException>(() => _productService.AddProduct(productAddRequest2));
    }
    //6. si el producto se guarda bien, su Id no debe ser Guid.Empty
    [Fact]
    public void AddProduct_ValidRequest_ProductIdIsNotEmpty()
    {
        // Arrange
        RequestProduct productAddRequest = new RequestProduct
        {
            Name = "Valid Product",
            Price = 20.0m,
        };
        // Act
        ProductResponse response = _productService.AddProduct(productAddRequest);
        // Assert
        Assert.NotEqual(Guid.Empty, response.Id);
    }
    #endregion

    #region GetProductById

    [Fact]
    //1. si el Id es Guid.Empty, debe lanzar una excepción
    public void GetProductById_EmptyGuid_ThrowsArgumentException()
    {
        // Arrange
        Guid id = Guid.Empty;
        // Act & Assert
        Assert.Throws<ArgumentException>(() => _productService.GetProductById(id));
    }
    [Fact]
    public void GetProductById_ValidId_ReturnsProductResponse()
    {
        // Arrange
        RequestProduct productAddRequest = new RequestProduct
        {
            Name = "Test Product",
            Price = 10.0m,
        };
        ProductResponse addedProduct = _productService.AddProduct(productAddRequest);
        
        // Act
        ProductResponse? response = _productService.GetProductById(addedProduct.Id);
        
        // Assert
        Assert.NotNull(response);
        Assert.Equal(addedProduct.Id, response.Id);
    }
    #endregion

    #region GetProducts
    [Fact]
    //1. la lista de productos debe estar vacia por defecto, antes de añadir cualquier producto
    public void GetProducts_EmptyCollection_()
    {
        IEnumerable<ProductResponse> productResponse =_productService.GetProducts();

        Assert.Empty(productResponse);
    }
    //2. retornar los objetos que se agregaron en simultaneo //Assert.contains
    #endregion

    #region UpdateProduct
    #endregion
    #region DeleteProduct
    #endregion
}
