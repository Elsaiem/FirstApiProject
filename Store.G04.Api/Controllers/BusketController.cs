using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.G04.Api.Attributes;
using Store.G04.Api.Errors;
using Store.G04.Core.Dtos.Busket;
using Store.G04.Core.Entities;
using Store.G04.Core.RepositoreContract;

namespace Store.G04.Api.Controllers
{


   

    public class BusketController : BaseApiController
    {
        private readonly IBusketRepository _BusketRepository ;
        private readonly IMapper _mapper;

        public BusketController(IBusketRepository busketRepository,IMapper mapper)
        {
            _BusketRepository = busketRepository;
            _mapper = mapper;
        }

       

        [HttpGet]
        [CachedAttribute(300)]
        public async Task<ActionResult<CustomerBusket>> GetBusket(string? Id)
        {
            if (Id == null)
            {
                return BadRequest(new ApiErrorResponse(400,"Invalid Id!!"));


            }


           var busket= await  _BusketRepository.GetBusketAsync(Id);
            if (busket == null)
            {
                new CustomerBusket()
                {
                    Id = Id,
                };
              

            }

            return Ok(busket);
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBusket>>CreateOrUpdateBusket(CustomerBusketDto model)
        {




         var busket= await _BusketRepository.UpdateBusketAsync(_mapper.Map<CustomerBusket>(model));
            if (busket == null) return BadRequest(new ApiErrorResponse(400));
            return Ok(busket);



        }
        [HttpDelete]
       public async Task DeleteBusket(string id)
        {

             await _BusketRepository.DeleteBusketAsync(id);
        }







    }
}
