using fisrt_project_api.Models.domain;

namespace fisrt_project_api.Repositories.Interface
{
    public interface ICategory
    {
        Task<Category> CreateAsync(Category category); 
        
        Task<IEnumerable<Category>> GetAllAsync();

        Task<Category?> GetCategoryById( Guid id);

       Task<Category?> UpdateAsync(Category category);

        Task<Category?> DeleteCategory(Guid id);

    }
}
