using Store.G04.Core.Dtos;
using Store.G04.Core.Dtos.Products;
using Store.G04.Core.Helper;
using Store.G04.Core.Specification.ProductsSpec;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G04.Core.ServicesContract
{
    public interface IServiceProduct
    {
     Task<PaginationResponse<ProductDto>>   GetAllProductAsync(ProductSpecParams productSpec);
     Task<IEnumerable<TypeBrandDto>>   GetAllTypesAsync();
     Task<IEnumerable<TypeBrandDto>>   GetAllBrandsAsync();

     Task<ProductDto>   GEtProductById(int id);



    }
}
