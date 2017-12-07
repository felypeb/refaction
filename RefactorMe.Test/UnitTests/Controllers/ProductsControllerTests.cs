using System;
using System.Threading.Tasks;
using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RefactorMe.Application.ApiModels;
using RefactorMe.Application.Interfaces;
using RefactorMe.Webapi.Controllers;

// ReSharper disable ExpressionIsAlwaysNull

namespace RefactorMe.Test.UnitTests.Controllers
{
    [TestClass]
    public class ProductsControllerTests
    {
        private Mock<IProductAppService> _mockProductAppService;
        private Mock<IProductOptionAppService> _mockProductOptionAppService;
        private MockRepository _mockRepository;
        private ProductsController _productsController;

        [TestCleanup]
        public void TestCleanup()
        {
            this._mockRepository.VerifyAll();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            this._mockRepository = new MockRepository(MockBehavior.Strict);

            this._mockProductAppService = this._mockRepository.Create<IProductAppService>();
            this._mockProductOptionAppService = this._mockRepository.Create<IProductOptionAppService>();

            this._productsController = new ProductsController(this._mockProductAppService.Object,
                                                              this._mockProductOptionAppService.Object);
        }

        [TestMethod]
        public async Task GetProductById_WhenProductNotExists_ShouldReturnNotFound()
        {
            ProductApiModel productMock = null;
            this._mockProductAppService.Setup(p => p.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(productMock);

            var testResult = await this._productsController.GetProductById(Guid.NewGuid());

            Assert.IsInstanceOfType(testResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task GetProductById_WhenProductExists_ShouldReturnProduct()
        {
            var productMock = new ProductApiModel();
            this._mockProductAppService.Setup(p => p.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(productMock);

            var testResult = await this._productsController.GetProductById(Guid.NewGuid());

            Assert.IsInstanceOfType(testResult, typeof(OkNegotiatedContentResult<ProductApiModel>));
        }

        [TestMethod]
        public async Task ListProduct_ShouldReturnProductList()
        {
            var productMock = new ProductsApiModel();
            this._mockProductAppService.Setup(p => p.ListAsync()).ReturnsAsync(productMock);

            var testResult = await this._productsController.ListProduct();

            Assert.IsInstanceOfType(testResult, typeof(OkNegotiatedContentResult<ProductsApiModel>));
        }

        [TestMethod]
        public async Task ListProductByName_ShouldReturnProductListFilteredByName()
        {
            var productMock = new ProductsApiModel();
            this._mockProductAppService.Setup(p => p.ListByNameAsync(It.IsAny<string>())).ReturnsAsync(productMock);

            var testResult = await this._productsController.ListProductByName("test");

            Assert.IsInstanceOfType(testResult, typeof(OkNegotiatedContentResult<ProductsApiModel>));
        }

        [TestMethod]
        public async Task CreateProduct_WhenProductNotExists_ShouldReturnCreated()
        {
            var productMock = new ProductApiModel();
            this._mockProductAppService.Setup(p => p.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(null as ProductApiModel);
            this._mockProductAppService.Setup(p => p.CreateAsync(It.IsAny<ProductApiModel>())).ReturnsAsync(productMock);

            var testResult = await this._productsController.CreateProduct(productMock);

            Assert.IsInstanceOfType(testResult, typeof(CreatedAtRouteNegotiatedContentResult<ProductApiModel>));
        }

        [TestMethod]
        public async Task CreateProduct_WhenProductExists_ShouldReturnConflict()
        {
            var productMock = new ProductApiModel();
            this._mockProductAppService.Setup(p => p.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(productMock);

            var testResult = await this._productsController.CreateProduct(productMock);

            Assert.IsInstanceOfType(testResult, typeof(ConflictResult));
        }

        [TestMethod]
        public async Task UpdateProduct_WhenProductExists_ShouldReturnOk()
        {
            var productMock = new ProductApiModel();
            this._mockProductAppService.Setup(p => p.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(productMock);
            this._mockProductAppService.Setup(p => p.UpdateAsync(It.IsAny<ProductApiModel>())).Returns(Task.CompletedTask);

            var testResult = await this._productsController.UpdateProduct(Guid.NewGuid(), productMock);

            Assert.IsInstanceOfType(testResult, typeof(OkResult));
        }

        [TestMethod]
        public async Task UpdateProduct_WhenProductNotExists_ShouldReturnNotFound()
        {
            var productMock = new ProductApiModel();
            this._mockProductAppService.Setup(p => p.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(null as ProductApiModel);

            var testResult = await this._productsController.UpdateProduct(Guid.NewGuid(), productMock);

            Assert.IsInstanceOfType(testResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task RemoveProduct_WhenProductExists_ShouldReturnOk()
        {
            var productMock = new ProductApiModel();
            this._mockProductAppService.Setup(p => p.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(productMock);
            this._mockProductAppService.Setup(p => p.RemoveAsync(It.IsAny<ProductApiModel>())).Returns(Task.CompletedTask);

            var testResult = await this._productsController.RemoveProductById(Guid.NewGuid());

            Assert.IsInstanceOfType(testResult, typeof(OkResult));
        }

        [TestMethod]
        public async Task RemoveProduct_WhenProductNotExists_ShouldReturnNotFound()
        {
            this._mockProductAppService.Setup(p => p.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(null as ProductApiModel);

            var testResult = await this._productsController.RemoveProductById(Guid.NewGuid());

            Assert.IsInstanceOfType(testResult, typeof(NotFoundResult));
        }

        //Todo: (create tests for the rest of the functions)
    }
}