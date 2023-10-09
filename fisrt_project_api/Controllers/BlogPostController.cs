using fisrt_project_api.Models.domain;
using fisrt_project_api.Models.DTO;
using fisrt_project_api.Repositories.Implementation;
using fisrt_project_api.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace fisrt_project_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostController : ControllerBase
    {
        private readonly IBlogPost blogPostRepository;
        private readonly ICategory categoryRepository;

        public BlogPostController(IBlogPost blogPostRepository, ICategory categoryRepository)
        {
            this.blogPostRepository= blogPostRepository;
            this.categoryRepository = categoryRepository;
        }


        [HttpPost]
        public async Task<IActionResult> CreateBlogPost([FromBody] CreateBlogPostRequestDto request)
        {

            var BlogPost = new BlogPost
            {
                Author = request.Author,
                Content = request.Content,
                FeaturedImageUrl = request.FeaturedImageUrl,
                Isvisible = request.Isvisible,
                PublishedDate = request.PublishedDate,
                ShortDescription = request.ShortDescription,
                Title = request.Title,  
                UrlHandle = request.UrlHandle,
                Categories=new List<Category>()

            };

            foreach(var categoryGuid in request.Categories) 
            {
                var existingCategory = await categoryRepository.GetCategoryById(categoryGuid);  
                if (existingCategory != null) 
                {
                    BlogPost.Categories.Add(existingCategory);  
                }
            }


            BlogPost=await blogPostRepository.CreateAsync(BlogPost);

            var respone = new BlogPostDto
            {
                Id=BlogPost.Id,


                Author = BlogPost.Author,
                Content = BlogPost.Content,
                FeaturedImageUrl = BlogPost.FeaturedImageUrl,
                Isvisible = BlogPost.Isvisible,
                PublishedDate = BlogPost.PublishedDate,
                ShortDescription = BlogPost.ShortDescription,
                Title = BlogPost.Title,
                UrlHandle = BlogPost.UrlHandle,
                Categories = BlogPost.Categories.Select(x => new CategoryDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle,
                }).ToList()

            };
            return Ok(respone);
           

        }

        [HttpGet]
        public async Task<IActionResult> GetAllBlogPost()
        {

            var BlogPosts = await blogPostRepository.GetAllAsync();

            var response = new List<BlogPostDto>();

            foreach (var blogpost in BlogPosts)
            {
                response.Add(new BlogPostDto
                {  Id = blogpost.Id,
                    Author = blogpost.Author,
                    Content = blogpost.Content,
                    Title = blogpost.Title, 
                    UrlHandle = blogpost.UrlHandle, 
                    ShortDescription= blogpost.ShortDescription,
                    FeaturedImageUrl= blogpost.FeaturedImageUrl,
                    Isvisible= blogpost.Isvisible,
                    PublishedDate = blogpost.PublishedDate,
                    Categories = blogpost.Categories.Select(x => new CategoryDto
                    {
                        Id = x.Id,
                        Name = x.Name,
                        UrlHandle = x.UrlHandle,
                    }).ToList()



                });

            }

            return Ok(response);

        }


    }
}
