using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using RefactorMe.Application.ApiModels;
using RefactorMe.Application.Interfaces;

namespace RefactorMe.Webapi.Controllers
{
    [RoutePrefix("products")]
    public class ProductsController : ApiController
    {
        private readonly IProductAppService _productAppService;
        private readonly IProductOptionAppService _productOptionAppService;

        public ProductsController(IProductAppService productAppService, IProductOptionAppService productOptionAppService)
        {
            this._productAppService = productAppService;
            this._productOptionAppService = productOptionAppService;
        }

        [Route]
        [HttpGet]
        [ResponseType(typeof(ProductsApiModel))]
        public async Task<IHttpActionResult> ListProduct()
        {
            return this.Ok(await this._productAppService.ListAsync());
        }

        [Route("{name:alpha}")]
        [HttpGet]
        public async Task<IHttpActionResult> ListProductByName(string name)
        {
            return this.Ok(await this._productAppService.ListByNameAsync(name));
        }

        [Route("{id:guid}", Name = "GetById")]
        [HttpGet]
        [ResponseType(typeof(ProductApiModel))]
        public async Task<IHttpActionResult> GetProductById(Guid id)
        {
            var product = await this._productAppService.GetByIdAsync(id);

            if (product == null)
                return this.NotFound();

            return this.Ok(product);
        }

        [Route]
        [HttpPost]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> CreateProduct(ProductApiModel product)
        {
            if (!this.ModelState.IsValid)
                return this.BadRequest(this.ModelState);

            if (await this._productAppService.GetByIdAsync(product.Id) != null)
                return this.Conflict();

            var newProduct = await this._productAppService.CreateAsync(product);

            return this.CreatedAtRoute("GetById", new { id = product.Id }, newProduct);
        }

        [Route("{id:guid}")]
        [HttpPut]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> UpdateProduct(Guid id, ProductApiModel product)
        {
            if (!this.ModelState.IsValid)
                return this.BadRequest(this.ModelState);

            if (await this._productAppService.GetByIdAsync(id) == null)
                return this.NotFound();

            product.Id = id;
            await this._productAppService.UpdateAsync(product);

            return this.Ok();
        }

        [Route("{id:guid}")]
        [HttpDelete]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> RemoveProductById(Guid id)
        {
            var product = await this._productAppService.GetByIdAsync(id);

            if (product == null)
                return this.NotFound();

            await this._productAppService.RemoveAsync(product);

            return this.Ok();
        }

        [Route("{productId:guid}/options")]
        [HttpGet]
        [ResponseType(typeof(ProductOptionsApiModel))]
        public async Task<IHttpActionResult> ListOptions(Guid productId)
        {
            var listByProductId = await this._productOptionAppService.ListByProductIdAsync(productId);

            return this.Ok(listByProductId);
        }

        [Route("{productId:guid}/options/{id:guid}", Name = "GetOptionById")]
        [HttpGet]
        [ResponseType(typeof(ProductOptionApiModel))]
        public async Task<IHttpActionResult> GetOptionById(Guid productId, Guid id)
        {
            var productOptionApiModel = await this._productOptionAppService.GetByIdAsync(productId, id);

            return this.Ok(productOptionApiModel);
        }

        [Route("{productId:guid}/options")]
        [HttpPost]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> CreateOption(Guid productId, ProductOptionApiModel option)
        {
            if (!this.ModelState.IsValid)
                return this.BadRequest(this.ModelState);

            if (await this._productOptionAppService.GetByIdAsync(productId, option.Id) != null)
                return this.Conflict();

            option.ProductId = productId;
            var newOption = await this._productOptionAppService.CreateAsync(option);

            return this.CreatedAtRoute("GetOptionById", new { productId = newOption.ProductId, id = newOption.Id }, newOption);
        }

        [Route("{productId:guid}/options/{id:guid}")]
        [HttpPut]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> UpdateOption(Guid productId, ProductOptionApiModel option)
        {
            if (!this.ModelState.IsValid)
                return this.BadRequest(this.ModelState);

            if (await this._productOptionAppService.GetByIdAsync(productId, option.Id) == null)
                return this.NotFound();

            option.Id = productId;
            await this._productOptionAppService.UpdateAsync(option);

            return this.Ok();
        }

        [Route("{productId:guid}/options/{id:guid}")]
        [HttpDelete]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> RemoveOptionById(Guid productId, Guid optionId)
        {
            var option = await this._productOptionAppService.GetByIdAsync(productId, optionId);

            if (option == null)
                return this.NotFound();

            await this._productOptionAppService.RemoveAsync(option);

            return this.Ok();
        }
    }
}
