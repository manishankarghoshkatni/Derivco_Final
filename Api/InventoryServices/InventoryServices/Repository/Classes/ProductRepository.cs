using System;
using System.Linq;
using InventoryServices.Repository.Interfaces;
using System.Threading.Tasks;
using InventoryServices.Shared;
using Newtonsoft.Json;
using System.Data.Entity;

namespace InventoryServices.Repository.Classes
{
    public class ProductRepository : IProductRepository
    {
        private InventoryEntities db = new InventoryEntities();

        public string GetAll()
        {
            string jsonResponse = "";

            try
            {
                var q = from p in db.Products
                        orderby p.ProductName 
                        select new
                        {
                            ProductId = p.ProductId,
                            ProductName = p.ProductName,
                            ProductDescription = p.ProductDescription,
                            CategoryId = p.CategoryId,
                            CategoryName=p.Category.CategoryName,
                            CategoryDescription=p.Category.CategoryDescription,
                            Price = p.Price,
                            Currency = p.Currency,
                            UnitId=p.Unit.UnitId,
                            UnitName=p.Unit.UnitName,
                            UnitDescription=p.Unit.UnitDescription 
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
            catch (Exception ex)
            {
                jsonResponse = JsonConvert.SerializeObject(Helper.CreateErrorResponse(ex));
            }

            return jsonResponse;
        }

        public async Task<string> GetByIdAsync(int id)
        {
            string jsonResponse = "";

            try
            {
                Product p = await db.Products.FindAsync(id);
                if (p != null)
                {
                    var result = new
                    {
                        ProductId = p.ProductId,
                        ProductName = p.ProductName,
                        ProductDescription = p.ProductDescription,
                        CategoryId = p.CategoryId,
                        CategoryName = p.Category.CategoryName,
                        CategoryDescription = p.Category.CategoryDescription,
                        Price = p.Price,
                        Currency = p.Currency,
                        UnitId = p.Unit.UnitId,
                        UnitName = p.Unit.UnitName,
                        UnitDescription = p.Unit.UnitDescription
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

        public async Task<string> GetByNameAsync(string productName)
        {
            string jsonResponse = "";

            try
            {
                var q = db.GetProductByName(productName);
                
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
            catch (Exception ex)
            {
                jsonResponse = JsonConvert.SerializeObject(Helper.CreateErrorResponse(ex));
            }

            return jsonResponse;
        }

        public async Task<string> GetByCategoryAsync(int categoryId)
        {
            string jsonResponse = "";

            try
            {
                var q = db.GetProductByCategoryId(categoryId);

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
            catch (Exception ex)
            {
                jsonResponse = JsonConvert.SerializeObject(Helper.CreateErrorResponse(ex));
            }

            return jsonResponse;
        }

        public async Task<string> CreateProductAsync(Product product)
        {
            string jsonResponse = "";

            try
            {
                var temp = (from p in db.Products
                            where p.ProductName.Equals(product.ProductName)
                            select p).FirstOrDefault();
                if (temp == null)
                {
                    db.Products.Add(product);
                    await db.SaveChangesAsync();
                    jsonResponse = JsonConvert.SerializeObject(Helper.CreateDataResponse(product.ProductId.ToString()));
                }
                else
                {
                    jsonResponse = JsonConvert.SerializeObject(Helper.CreateErrorResponse(new Exception("Product already exists!")));
                }
            }
            catch (Exception ex)
            {
                jsonResponse = JsonConvert.SerializeObject(Helper.CreateErrorResponse(ex));
            }

            return jsonResponse;
        }

        public async Task<string> ModifyProductAsync(Product product)
        {
            string jsonResponse = "";

            try
            {
                db.Entry(product).State = EntityState.Modified;
                await db.SaveChangesAsync();
                jsonResponse = JsonConvert.SerializeObject(Helper.CreateDataResponse(product.ProductId.ToString()));
            }
            catch (Exception ex)
            {
                if (!this.ProductExists(product.ProductId))
                {
                    jsonResponse = JsonConvert.SerializeObject(Helper.CreateErrorResponse(new Exception("Product Id not available")));
                }
                else jsonResponse = JsonConvert.SerializeObject(Helper.CreateErrorResponse(ex));
            }

            return jsonResponse;
        }

        public async Task<string> DeleteProductAsync(int productId)
        {
            string jsonResponse = "";

            try
            {
                Product product = await db.Products.FindAsync(productId);
                if (product == null)
                {
                    jsonResponse = JsonConvert.SerializeObject(Helper.CreateNoDataResponse());
                }
                else
                {
                    db.Products.Remove(product);
                    await db.SaveChangesAsync();
                    jsonResponse = JsonConvert.SerializeObject(Helper.CreateDataResponse("Product Deleted"));
                }
            }
            catch (Exception ex)
            {
                jsonResponse = JsonConvert.SerializeObject(Helper.CreateErrorResponse(ex));
            }

            return jsonResponse;
        }

        private bool ProductExists(int id)
        {
            return db.Products.Count(e => e.ProductId == id) > 0;
        }

        public void Cleanup()
        {
            db.Dispose();
        }
    }
}