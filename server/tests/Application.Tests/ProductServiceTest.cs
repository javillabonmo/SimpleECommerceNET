

using Application.DTOs;

namespace Application.Tests;
using Application.Services.Interfaces;
using Application.Services;
using Application.DTOs.Enums;
using Xunit.Abstractions;

public class ProductServiceTest
{
    /*
     * Asegura que las reglas de negocio y seguridad se cumplan
     * verificar que se lancen las excepciones correctas
     * 1.Cuando ProductRequest.Name es null, se lanza ArgumentException
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

    private readonly ITestOutputHelper _testOutputHelper;
    public  ProductServiceTest(ITestOutputHelper testOutputHelper)
    {
        _productService =  new ProductService();
        _testOutputHelper = testOutputHelper;
        //mockear el servicio de categorías,products si es necesario

    }

    #region AddProduct


    //cada requerimiento es una funcion de test
    //MétodoAEvaluar_CondiciónEsperada_ResultadoEsperado

    //2. si ProductRequest es nulo, debe lanzar una excepción
    [Fact]
    public void AddProduct_NullRequest_ThrowsArgumentNullException()
    {
        // Arrange
        ProductRequest? productAddRequest = null;
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => _productService.AddProduct(productAddRequest));
    }
    //3. si Name es nulo o vacío, debe lanzar una excepción
    [Fact]
    public void AddProduct_NullOrEmptyName_ThrowsArgumentException()
    {
        // Arrange
        ProductRequest productAddRequest = new ProductRequest
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
        ProductRequest productAddRequest = new ProductRequest
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
        ProductRequest productAddRequest1 = new ProductRequest
        {
            Name = "Test Product",
            Price = 10.0m,
            CategoryId = Guid.NewGuid(), // Assuming CategoryId is required
        };
        _productService.AddProduct(productAddRequest1); // Add first product
        ProductRequest productAddRequest2 = new ProductRequest
        {
            Name = "Test Product", // Duplicate name
            Price = 15.0m,
            CategoryId = Guid.NewGuid(), // Assuming CategoryId is required
        };
        // Act & Assert
        Assert.Throws<ArgumentException>(() => _productService.AddProduct(productAddRequest2));
    }
    //6. si el producto se guarda bien, su Id no debe ser Guid.Empty
    [Fact]
    public void AddProduct_ValidRequest_ProductIdIsNotEmpty()
    {
        // Arrange
        ProductRequest productAddRequest = new ProductRequest
        {
            Name = "Valid Product",
            Price = 20.0m,
            CategoryId = Guid.NewGuid(), // Assuming CategoryId is required
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
        ProductRequest productAddRequest = new ProductRequest
        {
            Name = "Test Product",
            Price = 10.0m,
            CategoryId = Guid.NewGuid(), // Assuming CategoryId is required
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
    [Fact]
    public void UpdateProduct_ValidRequest_ProductUpdated()
    {
        // Arrange
        ProductRequest productAddRequest = new ProductRequest
        {
            Name = "Test Product",
            Price = 10.0m,
            CategoryId = Guid.NewGuid(), // Assuming CategoryId is required
        };
        ProductResponse addedProduct = _productService.AddProduct(productAddRequest);
        
        // Act
        addedProduct.Name = "Updated Product";
        ProductResponse? updatedProduct = _productService.UpdateProduct(addedProduct.ProductUpdateRequest());
        // Assert
        Assert.NotNull(updatedProduct);
        Assert.Equal("Updated Product", updatedProduct.Name);
    }

    [Fact]
    public void UpdateProduct_IdNotFound_ReturnsNull()
    {
        // Arrange
        ProductRequest productAddRequest = new ProductRequest
        {
            Name = "Test Product",
            Price = 10.0m,
            CategoryId = Guid.NewGuid(), // Assuming CategoryId is required
        };
        ProductResponse addedProduct = _productService.AddProduct(productAddRequest);
        
        // Act
        addedProduct.Id = Guid.NewGuid(); // Set to a non-existing ID
        ProductResponse? updatedProduct = _productService.UpdateProduct(addedProduct.ProductUpdateRequest());
        
        // Assert
        Assert.Null(updatedProduct);
    }

    [Fact]
    public void UpdateProduct_IdNull_ThrowsArgumentException()
    {
        // Arrange
        ProductRequest productAddRequest = new ProductRequest
        {
            Name = "Test Product",
            Price = 10.0m,
            CategoryId = Guid.NewGuid(), // Assuming CategoryId is required

        };
        ProductResponse addedProduct = _productService.AddProduct(productAddRequest);
        
        // Act & Assert
        addedProduct.Id = Guid.Empty; // Set to an empty ID
        Assert.Throws<ArgumentException>(() => _productService.UpdateProduct(addedProduct.ProductUpdateRequest()));
    }
    //3. si el producto a actualizar es igual al que se está actualizando, no debe lanzar una excepción, debe retornar el mismo objeto
    //4. el id del producto actualizado debeía ser el mismo que el del producto original
    [Fact]
    public void UpdateProduct_SameProduct_ReturnsSameProduct()
    {
        // Arrange
        ProductRequest productAddRequest = new ProductRequest
        {
            Name = "Test Product",
            Price = 10.0m,
            CategoryId = Guid.NewGuid(), // Assuming CategoryId is required
        };
        ProductResponse addedProduct = _productService.AddProduct(productAddRequest);
        
        // Act
        ProductResponse? updatedProduct = _productService.UpdateProduct(addedProduct.ProductUpdateRequest());
        
        // Assert
        Assert.NotNull(updatedProduct);
        Assert.Equal(addedProduct.Id, updatedProduct.Id);
    }
    #region DeleteProduct
    #endregion

    [Fact]
    public void DeleteProduct_ValidId_ProductDeleted()
    {
        // Arrange
        ProductRequest productAddRequest = new ProductRequest
        {
            Name = "Test Product",
            Price = 10.0m,
            CategoryId = Guid.NewGuid(), // Assuming CategoryId is required
        };
        ProductResponse addedProduct = _productService.AddProduct(productAddRequest);
        
        // Act
        bool isDeleted = _productService.DeleteProduct(addedProduct.Id);

        // Assert

        // With this corrected line:
        _productService.GetProducts().Where(product => product.Id == addedProduct.Id);
        Assert.True(isDeleted);

        if (_productService.GetProducts().FirstOrDefault(id => id.Equals(addedProduct.Id)) == null)
        {
            _testOutputHelper.WriteLine("Product deleted successfully.");
        }
        else
        {
            _testOutputHelper.WriteLine("Product deletion failed.");
        }
        
        Assert.Null(_productService.GetProductById(addedProduct.Id));
    }

    #region GetFilteredProducts

    [Fact]
    public void GetProducts_ValidCollection_ProductsFiltered() {


        //arrange
        ProductRequest productAddRequest = new ProductRequest
        {
            Name = "Test Product",
            Price = 10.0m,
            CategoryId = Guid.NewGuid(), // Assuming CategoryId is required
        };
        List<ProductResponse> addedProduct = [_productService.AddProduct(productAddRequest)];
        var products = _productService.GetFilteredProducts(nameof(productAddRequest.Name), "");
        var productResponse2 = _productService.GetProducts();


        foreach (ProductResponse product in products) {
            if (product.Name !=null)
            {
                if (product.Name.Contains("te", StringComparison.OrdinalIgnoreCase))
                {
                    Assert.Contains(product, addedProduct);
                }
            }    
        }
    }
    #endregion

    #region GetSortedProducts

    [Fact]
    public void GetSortedProducts_ValidCollection_ProductsSorted()
    {
        // Arrange
        ProductRequest productAddRequest1 = new ProductRequest
        {
            Name = "Apple",
            Price = 10.0m,
            CategoryId = Guid.NewGuid(), // Assuming CategoryId is required
        };
        ProductRequest productAddRequest2 = new ProductRequest
        {
            Name = "Banana",
            Price = 15.0m,
            CategoryId = Guid.NewGuid(), // Assuming CategoryId is required
        };
        _productService.AddProduct(productAddRequest1);
        _productService.AddProduct(productAddRequest2);

        var productsFromService = _productService.GetProducts();
        foreach (ProductResponse product in productsFromService)
        {
            _testOutputHelper.WriteLine(product.ToString());
            //_testOutputHelper.WriteLine($"Product Name: {product.Name}");
        }
        
        // Act
        IEnumerable<ProductResponse> sortedProducts = _productService.GetSortedProducts(productsFromService, nameof(ProductResponse.Name), SortOrderEnum.Ascending);
        // Assert
        var productList = sortedProducts.ToList();
        foreach (var product in productList)
        {
            
            _testOutputHelper.WriteLine($"Product Name: {product.Name}");
        }
        Assert.Equal("Apple", productList[0].Name);
        Assert.Equal("Banana", productList[1].Name);
    }
    #endregion
}
