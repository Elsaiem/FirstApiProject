using AutoMapper;
using Microsoft.Extensions.Configuration;
using Store.G04.Core.Dtos.Products;
using Store.G04.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G04.Core.Mapping.Products
{
    public class PictureUrlResolver : IValueResolver<Product, ProductDto, string>
    {
        private readonly IConfiguration _Configuration;
        public PictureUrlResolver(IConfiguration configuration)
        {
            _Configuration = configuration;
        }

       

        public string Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
        {
            if (!String.IsNullOrEmpty(source.PictureUrl)) {
                return $"{_Configuration["BaseURL"]}{source.PictureUrl}";
            
            }
            return string.Empty ;


        }
    }
}
