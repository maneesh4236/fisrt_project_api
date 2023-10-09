using fisrt_project_api.Data;
using fisrt_project_api.Models.domain;
using fisrt_project_api.Models.DTO;
using fisrt_project_api.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace fisrt_project_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private ICategory categoryRepository;

        public CategoryController(ICategory categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }



        [HttpPost]
        public async Task<IActionResult> CreatCategory(CreateCategoryRequestDto request)
        {
            //map DTO to domain model
            var category = new Category
            {
                Name = request.Name,
                UrlHandle = request.UrlHandle
            };

            await categoryRepository.CreateAsync(category);


            var response = new CategoryDto
            {
                Id = category.Id,
                Name = request.Name,
                UrlHandle = request.UrlHandle

            };

            return Ok(response);

        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategoris()
        {
            var categories = await categoryRepository.GetAllAsync();

            var response = new List<CategoryDto>();

            foreach (var category in categories)
            {
                response.Add(new CategoryDto
                { Id = category.Id,
                    Name = category.Name,
                    UrlHandle = category.UrlHandle

                });

            }

            return Ok(response);
        }
        //Get: by id
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetCategoryById([FromRoute] Guid id)
        {

            var existing =await categoryRepository.GetCategoryById(id);
            if(existing is null)
            {
                return NotFound();
            }

            var response = new CategoryDto();
            response.Id = existing.Id;
            response.Name = existing.Name;
            response.UrlHandle = existing.UrlHandle;
             

            return Ok(response);    

            
        }


        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> EditCategory([FromRoute] Guid id , UpdateCategoryRequestDto request )
        {
            var category=new Category();
            {
                category.Id = id;       
                category.Name = request.Name;
                category.UrlHandle = request.UrlHandle;
            }

            category =await categoryRepository.UpdateAsync(category);

            if (category is null) 
            {
                return NotFound();

             }
            var response = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle
            };

            return Ok(response);

        }


        //Delete
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] Guid id)
        {
            var category =await categoryRepository.DeleteCategory(id);
            if(category is null)
            {
                return NotFound();
            }
            var respons = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle

            };
            return Ok(respons);

            
        }

    }



}
