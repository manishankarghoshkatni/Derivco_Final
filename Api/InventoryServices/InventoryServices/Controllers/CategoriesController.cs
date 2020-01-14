using Newtonsoft.Json;
using System;
using System.Web.Http;
using InventoryServices.Repository.Interfaces;
using System.Threading.Tasks;
using InventoryServices.Shared;

namespace InventoryServices.Controllers
{
    [RoutePrefix("api/Categories")]
    public class CategoriesController : ApiController
    {
        ICategoryRepository CategoryRepository;

        public CategoriesController(ICategoryRepository CategoryRepository)
        {
            this.CategoryRepository = CategoryRepository;
        }

        /*
         * Get all Categories data
         * api/Category
         * */
        [Route("")]
        public string Get()
        {
            return CategoryRepository.GetAll();
        }

        /*
         * Get Category data for given catgegory id
         * api/Category/1
         * */
        [Route("{id:int}")]
        public async Task<string> Get(int id)
        {
            return await CategoryRepository.GetByIdAsync(id);
        }

        /*
         * Get Category data for given catgegory name
         * api/Category/Laptop
         * */
        [Route("{category}")]
        public async Task<string> Get(string category)
        {
            return await CategoryRepository.GetByNameAsync(category);
        }

        /*
         * Create new Category 
         * api/Category/Create/<Category object>
         * */
        [Route("Create")]
        [HttpPost]
        public async Task<string> CreateCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                return await CategoryRepository.CreateCategoryAsync(category);
            }
            else
            {
                string json = JsonConvert.SerializeObject(Helper.CreateErrorResponse(new Exception("Invalid Model data")));
                return json;
            }           
        }

        /*
         * Modify existing Category 
         * api/Category/Modify/<Category object>
         * */
        [Route("Modify")]
        [HttpPut]
        public async Task<string> ModifyCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                return await CategoryRepository.ModifyCategoryAsync(category);
            }
            else
            {
                string json = JsonConvert.SerializeObject(Helper.CreateErrorResponse(new Exception("Invalid Model data")));
                return json;
            }

        }

        /*
         * Delete Category data for given catgegory id
         * api/Category/Delete/1
         * */
        [Route("Delete/{id:int}")]
        [HttpDelete]
        public async Task<string> DeleteCategory(int id)
        {
            return await CategoryRepository.DeleteCategoryAsync(id);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                CategoryRepository.Cleanup();
            }
            base.Dispose(disposing);
        }
    }
}