using System.Threading.Tasks;
namespace InventoryServices.Repository.Interfaces
{
    public interface ICategoryRepository : ICommon
    {
        string GetAll();
        Task<string> GetByIdAsync(int id);
        Task<string> GetByNameAsync(string categoryName);
        Task<string> CreateCategoryAsync(Category category);
        Task<string> ModifyCategoryAsync(Category category);
        Task<string> DeleteCategoryAsync(int categoryId);
    }
}