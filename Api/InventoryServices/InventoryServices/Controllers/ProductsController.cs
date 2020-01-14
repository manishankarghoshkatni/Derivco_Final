using Newtonsoft.Json;
using System;
using System.Web.Http;
using InventoryServices.Repository.Interfaces;
using System.Threading.Tasks;
using InventoryServices.Shared;

namespace InventoryServices.Controllers
{
    [RoutePrefix("api/Products")]
    public class ProductsController : ApiController
    {
        IProductRepository productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        /*         
	     * Get all Products data
	     * api/Product
    	 * */
        [Route("")]
        public string Get()
        {
            return productRepository.GetAll();
        }

        /*
         * Get Product data for given product id
         * api/Product/1
         * */
        [Route("{id:int}")]
        public async Task<string> Get(int id)
        {
            return await productRepository.GetByIdAsync(id);
        }

        /*
         * Get Product data for given product name
         * api/Product/IBM Thinkpad
         * */
        [Route("{productName}")]
        public async Task<string> Get(string productName)
        {
            return await productRepository.GetByNameAsync(productName);
        }

        /*
         * Get Product data for given category id
         * api/Product/Category/1
         * */
        [Route("Category/{id:int}")]
        public async Task<string> GetForCategoryId(int id)
        {
            return await productRepository.GetByCategoryAsync(id);
        }

        /*
         * Create new Product 
         * api/Product/Create/<Product object>
         * */
        [Route("Create")]
        [HttpPost]
        public async Task<string> CreateProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                return await productRepository.CreateProductAsync(product);
            }
            else
            {
                string json = JsonConvert.SerializeObject(Helper.CreateErrorResponse(new Exception("Invalid Model data")));
                return json;
            }
        }

        /*
         * Modify existing Product 
         * api/Product/Modify/<Product object>
         * */
        [Route("Modify")]
        [HttpPut]
        public async Task<string> ModifyProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                return await productRepository.ModifyProductAsync(product);
            }
            else
            {
                string json = JsonConvert.SerializeObject(Helper.CreateErrorResponse(new Exception("Invalid Model data")));
                return json;
            }

        }

        /*
         * Delete Product data for given product id
         * api/Product/Delete/1
         * */
        [Route("Delete/{id:int}")]
        [HttpDelete]
        public async Task<string> DeleteProduct(int id)
        {
            return await productRepository.DeleteProductAsync(id);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                productRepository.Cleanup();
            }
            base.Dispose(disposing);
        }
    }
}

