using fisrt_project_api.Models.domain;

namespace fisrt_project_api.Repositories.Interface
{
    public interface IBlogPost
    {

      Task<BlogPost>  CreateAsync(BlogPost Blogpost);

      Task<IEnumerable<BlogPost>> GetAllAsync();
    }
}
