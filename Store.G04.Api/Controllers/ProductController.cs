using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.G04.Core.Dtos.Products;
using Store.G04.Core.Helper;
using Store.G04.Core.ServicesContract;
using Store.G04.Core.Specification.ProductsSpec;

namespace Store.G04.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly IServiceProduct _ServiceProduct;
        public ProductController(IServiceProduct serviceProduct)
        {
            _ServiceProduct = serviceProduct;
        }

     

        [HttpGet]
        
        public async Task<IActionResult> GetAllProduct([FromQuery] ProductSpecParams productSpec)//endPoint
        {

            return Ok(result);

        }
        [HttpGet("Brands")]
        public async Task<IActionResult> GetAllBrands()//endPoint
        {
            var result= await _ServiceProduct.GetAllBrandsAsync();

            return Ok(result);

        } 
        [HttpGet("Types")]
        public async Task<IActionResult> GetAllTypes()//endPoint
        {
            var result= await _ServiceProduct.GetAllTypesAsync();

            return Ok(result);

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int? id)//endPoint
        {
            if(id is null)
            {
                return BadRequest("InvalisID");
            }
            var result= await _ServiceProduct.GEtProductById(id.Value);
            if (result is null) { return NotFound($"the product with Id {id.Value} is not found"); }

            return Ok(result);

        }






    }
}
