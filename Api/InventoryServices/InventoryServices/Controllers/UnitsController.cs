using Newtonsoft.Json;
using System;
using System.Web.Http;
using InventoryServices.Repository.Interfaces;
using System.Threading.Tasks;
using InventoryServices.Shared;

namespace InventoryServices.Controllers
{
    [RoutePrefix("api/Units")]
    public class UnitsController : ApiController
    {
        IUnitRepository UnitRepository;

        public UnitsController(IUnitRepository UnitRepository)
        {
            this.UnitRepository = UnitRepository;
        }

        /*
	     * Get all Units data         
	     * api/Unit
    	 * */
        [Route("")]
        public string Get()
        {
            return UnitRepository.GetAll();
        }

        /*
         * Get Unit data for given unit id
         * api/Unit/1
         * */
        [Route("{id:int}")]
        public async Task<string> Get(int id)
        {
            return await UnitRepository.GetByIdAsync(id);
        }

        /*
         * Get Unit data for given unit name
         * api/Unit/IBM Thinkpad
         * */
        [Route("{unit}")]
        public async Task<string> Get(string unit)
        {
            return await UnitRepository.GetByNameAsync(unit);
        }

        /*
         * Create new Unit 
         * api/Unit/Create/<Unit object>
         * */
        [Route("Create")]
        [HttpPost]
        public async Task<string> CreateUnit(Unit unit)
        {
            if (ModelState.IsValid)
            {
                return await UnitRepository.CreateUnitAsync(unit);
            }
            else
            {
                string json = JsonConvert.SerializeObject(Helper.CreateErrorResponse(new Exception("Invalid Model data")));
                return json;
            }
        }

        /*
         * Modify existing Unit 
         * api/Unit/Modify/<Unit object>
         * */
        [Route("Modify")]
        [HttpPut]
        public async Task<string> ModifyUnit(Unit unit)
        {
            if (ModelState.IsValid)
            {
                return await UnitRepository.ModifyUnitAsync(unit);
            }
            else
            {
                string json = JsonConvert.SerializeObject(Helper.CreateErrorResponse(new Exception("Invalid Model data")));
                return json;
            }
        }

        /*
         * Delete Unit data for given unit id
         * api/Unit/Delete/1
         * */
        [Route("Delete/{id:int}")]
        [HttpDelete]
        public async Task<string> DeleteUnit(int id)
        {
            return await UnitRepository.DeleteUnitAsync(id);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                UnitRepository.Cleanup();
            }
            base.Dispose(disposing);
        }
    }
}
