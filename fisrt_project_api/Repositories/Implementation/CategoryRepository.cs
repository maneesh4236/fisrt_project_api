using fisrt_project_api.Data;
using fisrt_project_api.Models.domain;
using fisrt_project_api.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace fisrt_project_api.Repositories.Implementation
{
    public class CategoryRepository : ICategory

    {
        private ApllicationDbContext dbContext;

        public CategoryRepository(ApllicationDbContext dbContext)
        {
            this.dbContext=dbContext;
        }
        public async Task<Category> CreateAsync(Category category)
        {
            await dbContext.Categories.AddAsync(category);
            await dbContext.SaveChangesAsync();

            return category;
        }

        public async Task<Category?> DeleteCategory(Guid id)
        {
           var exsistingCategory= await dbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);    
            if(exsistingCategory is null) 
            {
                return null;
            }

            dbContext.Categories.Remove(exsistingCategory); 

            await dbContext.SaveChangesAsync(); 
            
            return exsistingCategory;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await dbContext.Categories.ToListAsync();
        }

        public async Task<Category?> GetCategoryById(Guid id)
        {
            return await dbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Category?> UpdateAsync(Category category)
        {
           var existingCategory= await dbContext.Categories.FirstOrDefaultAsync(x=>x.Id==category.Id);
            if (existingCategory!=null) 
            {      
                dbContext.Entry(existingCategory).CurrentValues.SetValues(category);
                await dbContext.SaveChangesAsync();
                return category;


            }
            return null;
        }
    }
}
