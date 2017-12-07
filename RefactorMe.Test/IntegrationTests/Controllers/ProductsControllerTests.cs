using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RefactorMe.Application.ApiModels;

namespace RefactorMe.Test.IntegrationTests.Controllers
{
    [TestClass]
    public class ProductsControllerTests
    {
        readonly HttpClient _client = new HttpClient();

        [TestCleanup]
        public void TestCleanup()
        {
        }

        [TestInitialize]
        public void TestInitialize()
        {
            this._client.BaseAddress = new Uri("http://localhost:58123");
        }

        [TestMethod]
        public async Task ListProduct_ShouldReturnProductList()
        {
            HttpResponseMessage response = await this._client.GetAsync("products");
            response.EnsureSuccessStatusCode();
            var products = await response.Content.ReadAsAsync<ProductsApiModel>();

            Assert.IsTrue(products.Items.Any());
        }
    }
}