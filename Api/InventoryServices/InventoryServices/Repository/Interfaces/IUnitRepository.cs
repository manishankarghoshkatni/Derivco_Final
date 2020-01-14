using System.Threading.Tasks;

namespace InventoryServices.Repository.Interfaces
{
    public interface IUnitRepository : ICommon
    {
        string GetAll();
        Task<string> GetByIdAsync(int id);
        Task<string> GetByNameAsync(string unitName);
        Task<string> CreateUnitAsync(Unit unit);
        Task<string> ModifyUnitAsync(Unit unit);
        Task<string> DeleteUnitAsync(int unitId);
    }
}