using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.G04.Api.Attributes;
using Store.G04.Api.Errors;

using Store.G04.Core.Dtos;
using Store.G04.Core.Dtos.Products;
using Store.G04.Core.Helper;
using Store.G04.Core.ServicesContract;
using Store.G04.Core.Specification.ProductsSpec;

namespace Store.G04.Api.Controllers
{
 
    public class ProductController : BaseApiController
    {

        private readonly IServiceProduct _ServiceProduct;
        public ProductController(IServiceProduct serviceProduct)
        {
            _ServiceProduct = serviceProduct;
        }




       
        [ProducesResponseType(typeof(PaginationResponse<ProductDto>),StatusCodes.Status200OK)]
        [HttpGet]
        [Cached(300)]
        [Authorize]
        public async Task<ActionResult<PaginationResponse<ProductDto>>> GetAllProduct([FromQuery] ProductSpecParams productSpec)//endPoint
        {

            var result=await _ServiceProduct.GetAllProductAsync(productSpec);
            return Ok(result);

        }


        [ProducesResponseType(typeof(IEnumerable<TypeBrandDto>), StatusCodes.Status200OK)]

        [HttpGet("Brands")]
        [Cached(300)]

        [Authorize]

        public async Task<ActionResult<IEnumerable<TypeBrandDto>>> GetAllBrands()//endPoint
        {
            var result= await _ServiceProduct.GetAllBrandsAsync();

            return Ok(result);

        }
        [ProducesResponseType(typeof(IEnumerable<TypeBrandDto>), StatusCodes.Status200OK)]

        [HttpGet("Types")]
        [Cached(300)]

        [Authorize]

        public async Task<ActionResult<IEnumerable<TypeBrandDto>>> GetAllTypes()//endPoint
        {
            var result= await _ServiceProduct.GetAllTypesAsync();

            return Ok(result);

        }

        [ProducesResponseType(typeof(IEnumerable<TypeBrandDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
     
        [HttpGet("{id}")]
        [Authorize]

        public async Task<IActionResult> GetProductById(int? id)//endPoint
        {
            if(id is null)
            {
                return BadRequest(new ApiErrorResponse(400));
            }
            var result= await _ServiceProduct.GEtProductById(id.Value);
            if (result is null) { return NotFound(new ApiErrorResponse(404,$"the product with Id {id.Value} is not found")); }

            return Ok(result);

        }






    }
}
