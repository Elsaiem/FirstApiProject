using AutoMapper;
using Store.G04.Core.Dtos.AuthDto;
using Store.G04.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G04.Core.Mapping.Auth
{
    public class AuthentictionProfile:Profile
    {
        public AuthentictionProfile()
        {
            CreateMap<Address,AddressDto>().ReverseMap();
        }


    }
}
