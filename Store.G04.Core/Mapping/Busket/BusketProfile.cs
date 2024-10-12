using AutoMapper;
using Store.G04.Core.Dtos.Busket;
using Store.G04.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G04.Core.Mapping.Busket
{
    public class BusketProfile: Profile
    {
        public BusketProfile() {

            CreateMap<CustomerBusket,CustomerBusketDto>().ReverseMap();
        
        
        
        }





    }
}
