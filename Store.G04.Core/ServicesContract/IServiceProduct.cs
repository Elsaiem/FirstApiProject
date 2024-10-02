using Store.G04.Core.Dtos;
using Store.G04.Core.Dtos.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G04.Core.ServicesContract
{
    public interface IServiceProduct
    {
     Task<IEnumerable<ProductDto>>   GetAllProductAsync();
     Task<IEnumerable<TypeBrandDto>>   GetAllTypesAsync();
     Task<IEnumerable<TypeBrandDto>>   GetAllBrandsAsync();

     Task<ProductDto>   GEtProductById(int id);



    }
}
