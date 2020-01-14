using System;
using System.Linq;
using InventoryServices.Repository.Interfaces;
using System.Threading.Tasks;
using InventoryServices.Shared;
using Newtonsoft.Json;
using System.Data.Entity;

namespace InventoryServices.Repository.Classes
{
    public class UnitRepository : IUnitRepository
    {
        private InventoryEntities db = new InventoryEntities();

        public string GetAll()
        {
            string jsonResponse = "";

            try
            {
                var q = from c in db.Units
                        orderby c.UnitName
                        select new
                        {
                            UnitId = c.UnitId,
                            UnitName = c.UnitName,
                            UnitDescription = c.UnitDescription
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
                Unit temp = await db.Units.FindAsync(id);
                if (temp != null)
                {
                    var result = new
                    {
                        UnitId = temp.UnitId,
                        UnitName = temp.UnitName,
                        UnitDescription = temp.UnitDescription
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


        public async Task<string> GetByNameAsync(string UnitName)
        {
            string jsonResponse = "";

            try
            {
                var q = from c in db.Units
                        where c.UnitName.ToUpper().Contains(UnitName)
                        orderby c.UnitName
                        select new
                        {
                            UnitId = c.UnitId,
                            UnitName = c.UnitName,
                            UnitDescription = c.UnitDescription
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
            catch (Exception ex)
            {
                jsonResponse = JsonConvert.SerializeObject(Helper.CreateErrorResponse(ex));
            }

            return jsonResponse;
        }

        public async Task<string> CreateUnitAsync(Unit unit)
        {
            string jsonResponse = "";

            try
            {
                var temp = (from u in db.Units
                            where u.UnitName.Equals(unit.UnitName)
                            select u).FirstOrDefault();
                if (temp == null)
                {
                    db.Units.Add(unit);
                    await db.SaveChangesAsync();
                    jsonResponse = JsonConvert.SerializeObject(Helper.CreateDataResponse(unit.UnitId.ToString()));
                }
                else
                {
                    jsonResponse = JsonConvert.SerializeObject(Helper.CreateErrorResponse(new Exception("Unit already exists!")));
                }
            }
            catch (Exception ex)
            {
                jsonResponse = JsonConvert.SerializeObject(Helper.CreateErrorResponse(ex));
            }

            return jsonResponse;
        }

        public async Task<string> ModifyUnitAsync(Unit Unit)
        {
            string jsonResponse = "";

            try
            {
                db.Entry(Unit).State = EntityState.Modified;
                await db.SaveChangesAsync();
                jsonResponse = JsonConvert.SerializeObject(Helper.CreateDataResponse(Unit.UnitId.ToString()));
            }
            catch (Exception ex)
            {
                if (!this.UnitExists(Unit.UnitId))
                {
                    jsonResponse = JsonConvert.SerializeObject(Helper.CreateErrorResponse(new Exception("Unit Id not available")));
                }
                else jsonResponse = JsonConvert.SerializeObject(Helper.CreateErrorResponse(ex));
            }

            return jsonResponse;
        }


        public async Task<string> DeleteUnitAsync(int UnitId)
        {
            string jsonResponse = "";

            try
            {
                Unit Unit = await db.Units.FindAsync(UnitId);
                if (Unit == null)
                {
                    jsonResponse = JsonConvert.SerializeObject(Helper.CreateNoDataResponse());
                }
                else if (this.ProductExists(UnitId))
                {
                    jsonResponse = JsonConvert.SerializeObject(Helper.CreateErrorResponse(new Exception("Can not delete Category because 1 or more product(s) exits for this unit")));
                }
                else
                {
                    db.Units.Remove(Unit);
                    await db.SaveChangesAsync();
                    jsonResponse = JsonConvert.SerializeObject(Helper.CreateDataResponse("Unit Deleted"));
                }
            }
            catch (Exception ex)
            {
                jsonResponse = JsonConvert.SerializeObject(Helper.CreateErrorResponse(ex));
            }

            return jsonResponse;
        }

        private bool ProductExists(int unitId)
        {
            return db.Products.Where(p => p.UnitId == unitId).FirstOrDefault() != null;
        }

        private bool UnitExists(int id)
        {
            return db.Units.Count(e => e.UnitId == id) > 0;
        }

        private bool UnitNameExists(string unitName)
        {
            return db.Units.Count(e => e.UnitName.Equals(unitName)) > 0;
        }

        public void Cleanup()
        {
            db.Dispose();
        }
    }
}