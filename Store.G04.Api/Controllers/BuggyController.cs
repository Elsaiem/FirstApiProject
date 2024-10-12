using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.G04.Api.Errors;
using Store.G04.Repository.Data.Contexts;

namespace Store.G04.Api.Controllers
{

    public class BuggyController :BaseApiController
    {
        private readonly StoreDbContext _Context;
        public BuggyController(StoreDbContext storeDbContext)
        {
            _Context = storeDbContext;
        }


        [HttpGet("notFound")]
        public async Task<IActionResult> GetNotFoundRequestError()
        {

            var brand = await _Context.ProductBrands.FindAsync(100);
            if (brand == null)
            {
                return NotFound(new ApiErrorResponse(404));


            }
            return Ok(brand);
             

        }



        [HttpGet("serverError")]
        public async Task<IActionResult> GetServerError() {
            var brand = await _Context.ProductBrands.FindAsync(100);
            
            var brandToString=brand.ToString();

            return Ok(brand);


        }
        [HttpGet("badRequest")]
        public async Task<IActionResult> GetBadRequestError()
        {
            return BadRequest(new ApiErrorResponse(400));
        }
        [HttpGet("badRequest/{id}")]
        public async Task<IActionResult> GetBadRequestError(int id)
        {
            return Ok();
        }
        [HttpGet("unAuthorized")]
        public async Task<IActionResult> GEtUnAuthorizedError(int id)
        {
            return  Unauthorized(new ApiErrorResponse(401));


        }



    }
}
