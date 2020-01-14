using System;
using System.Linq;
using InventoryServices.Repository.Interfaces;
using System.Threading.Tasks;
using InventoryServices.Shared;
using Newtonsoft.Json;
using System.Data.Entity;

namespace InventoryServices.Repository.Classes
{
    public class CategoryRepository : ICategoryRepository
    {
        private InventoryEntities db = new InventoryEntities();

        public string GetAll()
        {
            string jsonResponse = "";

            try
            {
                var q = from c in db.Categories
                        orderby c.CategoryName 
                           select new
                           {
                               CategoryId = c.CategoryId,
                               CategoryName = c.CategoryName,
                               CategoryDescription = c.CategoryDescription
                           };

                var result = q.ToArray();
                if (result.Count() > 0)
                {
                    jsonResponse = JsonConvert.SerializeObject(Helper.CreateDataResponse(result));
                }
                else
                {
                    jsonResponse = JsonConvert.SerializeObject(Helper.CreateNoDataResponse());
                }
            }
            catch(Exception ex)
            {
                jsonResponse = JsonConvert.SerializeObject(Helper.CreateErrorResponse(ex));
            }

            return jsonResponse;
        }

        public async Task<string> GetByIdAsync(int id)
        {
            string jsonResponse="";

            try
            {
                Category temp = await db.Categories.FindAsync(id);
                if(temp!= null)
                {
                    var result = new
                    {
                        CategoryId = temp.CategoryId,
                        CategoryName = temp.CategoryName,
                        CategoryDescription = temp.CategoryDescription
                    };

                    jsonResponse = JsonConvert.SerializeObject(Helper.CreateDataResponse(result));
                }
                else
                {
                    jsonResponse = JsonConvert.SerializeObject(Helper.CreateNoDataResponse());
                }
            }
            catch (Exception ex)
            {
                jsonResponse = JsonConvert.SerializeObject(Helper.CreateErrorResponse(ex));
            }
            return jsonResponse;
        }


        public async Task<string> GetByNameAsync(string categoryName)
        {
            string jsonResponse = "";

            try
            {
                var q = from c in db.Categories
                        where c.CategoryName.ToUpper().Contains(categoryName)
                        orderby c.CategoryName 
                        select new
                        {
                            CategoryId = c.CategoryId,
                            CategoryName = c.CategoryName,
                            CategoryDescription = c.CategoryDescription
                        };

                var result = await Task.FromResult(q.ToList());
                if (result.Count() > 0)
                {
                    jsonResponse = JsonConvert.SerializeObject(Helper.CreateDataResponse(result));
                }
                else
                {
                    jsonResponse = JsonConvert.SerializeObject(Helper.CreateNoDataResponse());
                }
            }
            catch(Exception ex)
            {
                jsonResponse = JsonConvert.SerializeObject(Helper.CreateErrorResponse(ex));
            }

            return jsonResponse;
        }

        public async Task<string> CreateCategoryAsync(Category category)
        {
            string jsonResponse = "";

            try
            {
                var temp = (from c in db.Categories
                         where c.CategoryName.Equals(category.CategoryName)
                         select c).FirstOrDefault();
                if (temp == null)
                {
                    db.Categories.Add(category);
                    await db.SaveChangesAsync();
                    jsonResponse = JsonConvert.SerializeObject(Helper.CreateDataResponse(category.CategoryId.ToString()));
                }
                else
                {
                    jsonResponse = JsonConvert.SerializeObject(Helper.CreateErrorResponse(new Exception("Category already exists!")));
                }
            }
            catch (Exception ex)
            {
                jsonResponse = JsonConvert.SerializeObject(Helper.CreateErrorResponse(ex));
            }

            return jsonResponse;
        }

        public async Task<string> ModifyCategoryAsync(Category category)
        {
            string jsonResponse = "";

            try
            {
                db.Entry(category).State= EntityState.Modified;
                await db.SaveChangesAsync();
                jsonResponse = JsonConvert.SerializeObject(Helper.CreateDataResponse(category.CategoryId.ToString()));
            }
            catch (Exception ex)
            {
                if(!this.CategoryExists(category.CategoryId))
                {
                    jsonResponse = JsonConvert.SerializeObject(Helper.CreateErrorResponse(new Exception("Category Id not available")));
                }
                else jsonResponse = JsonConvert.SerializeObject(Helper.CreateErrorResponse(ex));
            }

            return jsonResponse;
        }

        
        public async Task<string> DeleteCategoryAsync(int categoryId)
        {
            string jsonResponse = "";

            try
            {
                Category category = await db.Categories.FindAsync(categoryId);
                if (category == null)
                {
                    jsonResponse = JsonConvert.SerializeObject(Helper.CreateNoDataResponse());
                }
                else if (this.ProductExists(categoryId))
                {
                    jsonResponse = JsonConvert.SerializeObject(Helper.CreateErrorResponse(new Exception("Can not delete Category because 1 or more product(s) exits for this category")));
                }
                else
                {
                    db.Categories.Remove(category);
                    await db.SaveChangesAsync();
                    jsonResponse = JsonConvert.SerializeObject(Helper.CreateDataResponse("Category Deleted"));
                }
            }
            catch (Exception ex)
            {
                jsonResponse = JsonConvert.SerializeObject(Helper.CreateErrorResponse(ex));
            }

            return jsonResponse;
        }

        private bool ProductExists(int categoryId)
        {
            return db.Products.Where(p => p.CategoryId == categoryId).FirstOrDefault() != null;
        }
        private bool CategoryExists(int id)
        {
            return db.Categories.Count(e => e.CategoryId == id) > 0;
        }

        public void Cleanup()
        {
            db.Dispose();
        }
    }
}