using AutoMapper;
using Ecom.API.Helper;
using Ecom.Core.DTO;
using Ecom.Core.Entities.Product;
using Ecom.Core.interfaces;
using Ecom.Core.Sharing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.API.Controllers
{

    public class ProductsController : BaseController
    {
        public ProductsController(IUnitOfWork work, IMapper mapper) : base(work, mapper)
        {

        }
        [HttpGet("get-all")]
        public async Task<IActionResult> get([FromQuery] ProductParams productParams)
        {
            try
            {
                var Product = await work.ProductRepositry.
                    GetAllAsync(productParams);
                var totalCount = await work.ProductRepositry.CountAsync();

                return Ok(new Pagination<ProductDTO>(productParams.PageNumber, productParams.pageSize, totalCount, Product));





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
                var product = await work.ProductRepositry.GetByIdAsync(id, x => x.Category, x => x.Photos);
                var result = mapper.Map<ProductDTO>(product);
                if (product is null)
                {
                    return BadRequest(new ResponseAPI(400));
                }
                return Ok(result);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpPost("Add-Product")]
        public async Task<IActionResult> Add(AddProductDTO productDTO)
        {
            try
            {
              await work.ProductRepositry.AddAsync(productDTO);
                return Ok(new ResponseAPI(200) );
              
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseAPI(400, ex.Message));
            }

        }
        [HttpPut("Update-Product")]
        public async Task<IActionResult> Update( UpdateProductDTO updateProductDTO)
        {
            try
            {
                await work.ProductRepositry.UpdateAsync(updateProductDTO);
                return Ok(new ResponseAPI(200));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseAPI(400, ex.Message));
            }
        }
        [HttpDelete("Delete-Product/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
               var product= await work.ProductRepositry.GetByIdAsync(id,x => x.Photos ,x=> x.Category);
                    await work.ProductRepositry.DeleteAsync(product);
                return Ok(new ResponseAPI(200));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseAPI(400, ex.Message));
            }
        }
        }
}
