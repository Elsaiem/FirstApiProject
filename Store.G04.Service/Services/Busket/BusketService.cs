using AutoMapper;
using Store.G04.Core.Dtos.Busket;
using Store.G04.Core.Entities;
using Store.G04.Core.RepositoreContract;
using Store.G04.Core.ServicesContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G04.Service.Services.Busket
{
    public class BusketService : IBusketService
    {
        private readonly IBusketRepository _busketRepository;
        private readonly IMapper _mapper;

        public BusketService(IBusketRepository busketRepository,IMapper mapper)
        {
            this._busketRepository = busketRepository;
            this._mapper = mapper;
        }

        public async Task<CustomerBusketDto?> GetBusketAsync(string BysketId)
        {
            var busket= await _busketRepository.GetBusketAsync(BysketId);
            if(busket == null) return _mapper.Map<CustomerBusketDto>(new CustomerBusket()
            {
                Id= BysketId,
            });
            return _mapper.Map<CustomerBusketDto?>(busket);
        }

        public async Task<CustomerBusketDto?> UpdateBusketAsync(CustomerBusketDto busketDto)
        {
          var busket= await _busketRepository.UpdateBusketAsync(_mapper.Map<CustomerBusket>(busketDto));
            if (busket is null) return null;
            return _mapper.Map<CustomerBusketDto?>(busket);
        }

        public async Task<bool> DeleteBusketAsync(string BusketId)
        {
           return await  _busketRepository.DeleteBusketAsync(BusketId);
        }
    }
}
