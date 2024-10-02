using Store.G04.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G04.Core.Specification.ProductsSpec
{
    public class ProductWithCountSpecification:BaseSpecification<Product,int>
    {
        public ProductWithCountSpecification(ProductSpecParams productSpec) : base(
         P =>
          (string.IsNullOrEmpty(productSpec.Search) || P.Name.ToLower().Contains(productSpec.Search))
            &&
         (!productSpec.BrandId.HasValue || productSpec.BrandId == P.BrandId)
         &
         (!productSpec.TypeId.HasValue || productSpec.TypeId == P.TypeId)

         )
        {

           
        }



    }
}
