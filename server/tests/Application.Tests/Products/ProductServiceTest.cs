namespace SimpleEcommerce.Application.Tests.Products
{

    using AutoFixture;

    using FluentAssertions;



    using Microsoft.EntityFrameworkCore;

    using Moq;

    using SimpleEcommerce.Core.Domain.RespositoryContracts;

    using SimpleECommerce.Core.DTOs;
    using SimpleECommerce.Core.Services;
    using SimpleECommerce.Core.ServicesContrats;
    using SimpleEcommerce.Core.Domain.Entities.Inventory;

    using Xunit.Abstractions;
    using SimpleECommerce.Core.Enums;

    public class ProductServiceTest
    {
        /*
         * Asegura que las reglas de negocio y seguridad se cumplan
         * verificar que se lancen las excepciones correctas
         * 1.Cuando ProductRequest.ProductName es null, se lanza ArgumentException
         * 2.Cuando el Price es 0, lanza excepción
         * Cuando el producto se guarda bien, su ProductId no debe ser Guid.Empty
         * Cuando se consulta por ID inexistente, devuelve null o lanza NotFoundException
         *
         * El objetivo es: validar que el servicio se comporte como debe bajo distintos escenarios
         * los test deben fallar si la lógica de negocio falla
         *
         *por test unitario se prueba una sola funcionalidad, un solo metodo del servicio, de un metodo se puede esperar tener
         *varias pruebas unitarias, cada una probando un caso diferente siendo este caso una funcionalidad/requerimiento del servicio
         *
         * las validaciones van en el servicio y se comprueban en los test
         */

        private readonly IProductService _productService;

        private readonly ITestOutputHelper _testOutputHelper;

        private readonly IFixture _fixture;

        private readonly Mock<IProductRepository> _productRepositoryMock;


        public ProductServiceTest(ITestOutputHelper testOutputHelper)
        {
            _productService = new ProductService(_productRepositoryMock.Object);
            _testOutputHelper = testOutputHelper;
            _fixture = new Fixture();
            _productRepositoryMock = new Mock<IProductRepository>();

        }

        #region AddProduct


        //cada requerimiento es una funcion de test
        //MétodoAEvaluar_CondiciónEsperada_ResultadoEsperado

        // 2. si ProductRequest es nulo, debe lanzar una excepción
        [Fact]
        public async Task AddProduct_NullRequest_ThrowsArgumentNullException()
        {
            // Arrange
            ProductRequest? productAddRequest = null;
            Product product = productAddRequest.ToProduct();
            // Mock
            _productRepositoryMock.Setup(repo => repo.AddProduct(It.IsAny<Product>())).ReturnsAsync(product);


            // Act-ion
            Func<Task> act = async () => await _productService.AddProduct(productAddRequest);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'productAddRequest')");
        }

        // 3. si ProductName es nulo o vacío, debe lanzar una excepción
        [Fact]
        public async Task AddProduct_NullOrEmptyName_ThrowsArgumentException()
        {
            // Arrange
            ProductRequest productAddRequest = _fixture.Build<ProductRequest>().With(p => p.ProductName == null).Create();
            // Act
            Func<Task> act = async () => await _productService.AddProduct(productAddRequest);

            // Assert
            await act.Should().ThrowAsync<ArgumentException>()
                .WithMessage("Product name cannot be null or empty. (Parameter 'ProductName')");
        }
        //4. si Price es menor o igual a cero, debe lanzar una excepción
        [Fact]
        public async Task AddProduct_NegativeOrZeroPrice_ThrowsArgumentException()
        {
            // Arrange
            ProductRequest productAddRequest = _fixture.Build<ProductRequest>().With(p => p.ProductName == null).With(p => p.Price == 0.0m).Create();
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(async () => await _productService.AddProduct(productAddRequest));
        }
        //5. si ProductName esta duplicado, debe lanzar una excepción
        [Fact]
        public async Task AddProduct_DuplicateName_ArgumentExceptionn()
        {
            // Arrange
            ProductRequest productAddRequest1 = _fixture.Build<ProductRequest>().With(p => p.Price == 10.0m).Create();
            await _productService.AddProduct(productAddRequest1); // Add first product
            ProductRequest productAddRequest2 = _fixture.Build<ProductRequest>().With(p => p.Price == 15.0m).Create();
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(async () => await _productService.AddProduct(productAddRequest2));
        }
        //6. si el producto se guarda bien, su ProductId no debe ser Guid.Empty
        [Fact]
        public async Task AddProduct_ValidRequest_ProductIdIsNotEmpty()
        {
            // Arrange
            ProductRequest productAddRequest = _fixture.Build<ProductRequest>().With(p => p.Price == 10.0m).Create();
            // Act
            ProductResponse response = await _productService.AddProduct(productAddRequest);
            // Assert
            Assert.NotEqual(Guid.Empty, response.ProductId);
        }
        #endregion

        #region GetProductById

        [Fact]
        //1. si el ProductId es Guid.Empty, debe lanzar una excepción
        public async Task GetProductById_EmptyGuid_ThrowsArgumentException()
        {
            // Arrange
            Guid id = Guid.Empty;
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(async () => await _productService.GetProductById(id));
        }
        [Fact]
        public async Task GetProductById_ValidId_ReturnsProductResponse()
        {
            // Arrange
            ProductRequest productAddRequest = _fixture.Build<ProductRequest>().With(p => p.Price == 10.0m).Create();
            ProductResponse addedProduct = await _productService.AddProduct(productAddRequest);

            // Act
            ProductResponse? response = await _productService.GetProductById(addedProduct.ProductId);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(addedProduct.ProductId, response.ProductId);
        }
        #endregion

        #region GetProducts
        [Fact]
        //1. la lista de productos debe estar vacia por defecto, antes de añadir cualquier producto
        public async Task GetProducts_EmptyCollection_()
        {
            IEnumerable<ProductResponse> productResponse = await _productService.GetProducts();

            Assert.Empty(productResponse);
        }
        //2. retornar los objetos que se agregaron en simultaneo //Assert.contains
        #endregion

        #region UpdateProduct
        #endregion
        [Fact]
        public async Task UpdateProduct_ValidRequest_ProductUpdated()
        {
            // Arrange
            ProductRequest productAddRequest = _fixture.Build<ProductRequest>().With(p => p.Price == 10.0m).Create();
            ProductResponse addedProduct = await _productService.AddProduct(productAddRequest);

            // Act
            addedProduct.ProductName = "Updated Product";
            ProductResponse? updatedProduct = await _productService.UpdateProduct(addedProduct.ToProductUpdateRequest());
            // Assert
            Assert.NotNull(updatedProduct);
            Assert.Equal("Updated Product", updatedProduct.ProductName);
        }

        [Fact]
        public void UpdateProduct_IdNotFound_ReturnsNull()
        {
            // Arrange
            // Act
            ProductResponse? updatedProduct = null;

            // Assert
            Assert.Null(updatedProduct);
        }

        [Fact]
        public async Task UpdateProduct_IdNull_ThrowsArgumentException()
        {
            // Arrange
            ProductRequest productAddRequest = _fixture.Build<ProductRequest>().With(p => p.Price == 10.0m).Create();
            ProductResponse addedProduct = await _productService.AddProduct(productAddRequest);

            // Act & Assert
            addedProduct.ProductId = Guid.Empty; // Set to an empty ID
            await Assert.ThrowsAsync<ArgumentException>(async () => await _productService.UpdateProduct(addedProduct.ToProductUpdateRequest()));
        }
        //3. si el producto a actualizar es igual al que se está actualizando, no debe lanzar una excepción, debe retornar el mismo objeto
        //4. el id del producto actualizado debeía ser el mismo que el del producto original
        [Fact]
        public async Task UpdateProduct_SameProduct_ReturnsSameProduct()
        {
            // Arrange
            ProductRequest productAddRequest = _fixture.Build<ProductRequest>().With(p => p.Price == 10.0m).Create();
            ProductResponse addedProduct = await _productService.AddProduct(productAddRequest);

            // Act
            ProductResponse? updatedProduct = await _productService.UpdateProduct(addedProduct.ToProductUpdateRequest());

            // Assert
            Assert.NotNull(updatedProduct);
            Assert.Equal(addedProduct.ProductId, updatedProduct.ProductId);
        }
        #region DeleteProduct
        #endregion

        [Fact]
        public async Task DeleteProduct_ValidId_ProductDeleted()
        {
            // Arrange
            ProductRequest productAddRequest = _fixture.Build<ProductRequest>().With(p => p.Price == 10.0m).Create();
            ProductResponse addedProduct = await _productService.AddProduct(productAddRequest);

            // Act
            bool isDeleted = await _productService.DeleteProduct(addedProduct.ProductId);

            // Assert

            // With this corrected line:
            IEnumerable<ProductResponse> products = await _productService.GetProducts();

            products.Where(product => product.ProductId == addedProduct.ProductId);

            Assert.True(isDeleted);

            if (products.FirstOrDefault(id => id.Equals(addedProduct.ProductId)) == null)
            {
                _testOutputHelper.WriteLine("Product deleted successfully.");
            }
            else
            {
                _testOutputHelper.WriteLine("Product deletion failed.");
            }

            Assert.Null(await _productService.GetProductById(addedProduct.ProductId));
        }

        #region GetFilteredProducts

        [Fact]
        public async Task GetProducts_ValidCollection_ProductsFiltered()
        {


            //arrange
            ProductRequest productAddRequest = _fixture.Build<ProductRequest>().With(p => p.Price == 10.0m).Create();
            ProductResponse addedProduct = await _productService.AddProduct(productAddRequest);
            var products = await _productService.GetFilteredProducts(nameof(productAddRequest.ProductName), "");
            var productResponse2 = await _productService.GetProducts();


            foreach (ProductResponse product in products)
            {
                if (product.ProductName != null)
                {
                    if (product.ProductName.Contains("te", StringComparison.OrdinalIgnoreCase))
                    {
                        Assert.Contains(addedProduct, products);
                    }
                }
            }
        }
        #endregion

        #region GetSortedProducts

        [Fact]
        public async Task GetSortedProducts_ValidCollection_ProductsSorted()
        {
            // Arrange
            ProductRequest productAddRequest1 = _fixture.Build<ProductRequest>().With(p => p.ProductName == "Apple").Create();
            ProductRequest productAddRequest2 = _fixture.Build<ProductRequest>().With(p => p.ProductName == "Banana").Create();
            await _productService.AddProduct(productAddRequest1);
            await _productService.AddProduct(productAddRequest2);

            var productsFromService = await _productService.GetProducts();
            foreach (ProductResponse product in productsFromService)
            {
                _testOutputHelper.WriteLine(product.ToString());
                //_testOutputHelper.WriteLine($"Product ProductName: {product.ProductName}");
            }

            // Act
            IEnumerable<ProductResponse> sortedProducts = _productService.GetSortedProducts(productsFromService, nameof(ProductResponse.ProductName), SortOrderEnum.Ascending);
            // Assert
            var productList = sortedProducts.ToList();
            foreach (var product in productList)
            {

                _testOutputHelper.WriteLine($"Product ProductName: {product.ProductName}");
            }
            Assert.Equal("Apple", productList[0].ProductName);
            Assert.Equal("Banana", productList[1].ProductName);
        }
        #endregion
    }

}

