using fisrt_project_api.Data;
using fisrt_project_api.Models.domain;
using fisrt_project_api.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace fisrt_project_api.Repositories.Implementation
{
    public class BlogpostRepository : IBlogPost
    {
        private readonly ApllicationDbContext dbContext;
        public BlogpostRepository(ApllicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<BlogPost> CreateAsync(BlogPost blogpost)
        {
            await dbContext.AddAsync(blogpost);
            await dbContext.SaveChangesAsync();
            return blogpost;
           
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {


            return await dbContext.BlogPosts.Include(x=>x.Categories).ToListAsync();
        }
    }
}
