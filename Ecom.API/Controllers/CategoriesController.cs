using AutoMapper;
using Ecom.API.Helper;
using Ecom.Core.DTO;
using Ecom.Core.Entities.Product;
using Ecom.Core.interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.API.Controllers
{

    public class CategoriesController : BaseController
    {
        public CategoriesController(IUnitOfWork work, IMapper mapper) : base(work, mapper)
        {
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> get()
        {
            try
            {
                var category = await work.CategoryReositry.GetAllAsync();
                if (category is null)

                    return BadRequest( new ResponseAPI(400));

                return Ok(category);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }
        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> getById(int id)
        {
            try
            {
                var category = await work.CategoryReositry.GetByIdAsync(id);
                if (category is null)

                    return BadRequest(new ResponseAPI(400, $"not Found category id={id}") );

                return Ok(category);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("add-category")]
        public async Task<IActionResult> add(CategoryDTO categoryDTO)
        {
            try
            {
                var category = mapper.Map<Category>(categoryDTO);
                await work.CategoryReositry.AddAsync(category);
                return Ok(new ResponseAPI(200, "Item has been Added"));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpPut("update-category")]
        public async Task<IActionResult> update(UpdateCatgoryDTO categoryDTO)
        {
            try
            {
                var category = mapper.Map<Category>(categoryDTO);
                await work.CategoryReositry.UpdateAsync(category);
                return Ok(new ResponseAPI(200, "Item has been Uptated"));

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }
        [HttpDelete("delete-category/{id}")]
        public async Task<IActionResult> delete(int id)
        {
            try
            {
                await work.CategoryReositry.DeleteAsync(id);
                return Ok(new ResponseAPI(200,"Item has been deleted") );
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}