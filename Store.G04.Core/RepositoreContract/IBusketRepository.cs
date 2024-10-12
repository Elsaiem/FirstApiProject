using Store.G04.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G04.Core.RepositoreContract
{
    public interface IBusketRepository
    {
        Task<CustomerBusket> GetBusketAsync(string busketID);
        Task<CustomerBusket> UpdateBusketAsync(CustomerBusket customerBusket);


        Task<bool> DeleteBusketAsync(string busketID);





    }
}
