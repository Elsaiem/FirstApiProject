using AutoMapper;
using Store.G04.Core;
using Store.G04.Core.Dtos;
using Store.G04.Core.Dtos.Products;
using Store.G04.Core.Entities;
using Store.G04.Core.ServicesContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G04.Service.Services.Products
{
    public class ProductService : IServiceProduct
    {

        private readonly IUnitOfWork _UnitOfWork;
        private readonly IMapper mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _UnitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<IEnumerable<ProductDto>> GetAllProductAsync()
        {
         return  mapper.Map<IEnumerable<ProductDto>>(await _UnitOfWork.Repository<Product, int>().GetAllAsync());
     
        }



        public async Task<IEnumerable<TypeBrandDto>> GetAllBrandsAsync()
        {
            return mapper.Map<IEnumerable<TypeBrandDto>>(await _UnitOfWork.Repository<ProductBrand, int>().GetAllAsync());

        }


        public async Task<IEnumerable<TypeBrandDto>> GetAllTypesAsync()
        {
            return mapper.Map<IEnumerable<TypeBrandDto>>(await _UnitOfWork.Repository<ProductType, int>().GetAllAsync());

        }

        public async Task<ProductDto> GEtProductById(int id)
        {
            return mapper.Map<ProductDto>( await _UnitOfWork.Repository<Product, int>().GetAsync(id));

        }
    }
}
