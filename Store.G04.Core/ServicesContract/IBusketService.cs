using Store.G04.Core.Dtos.Busket;
using Store.G04.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G04.Core.ServicesContract
{
    public interface IBusketService
    {
        Task<CustomerBusketDto?> GetBusketAsync(string BysketId);

        Task<CustomerBusketDto?> UpdateBusketAsync(CustomerBusketDto busketDto);

        Task<bool> DeleteBusketAsync(string BusketId);




    }



}
