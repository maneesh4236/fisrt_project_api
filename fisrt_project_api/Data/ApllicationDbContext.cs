using fisrt_project_api.Models.domain;
using Microsoft.EntityFrameworkCore;

namespace fisrt_project_api.Data
{
    public class ApllicationDbContext : DbContext
    {
        public ApllicationDbContext(DbContextOptions options) : base(options)
        {
            
        }
        public DbSet<BlogPost> BlogPosts { get; set; }

        public DbSet<Category> Categories { get; set; }
    }
}
