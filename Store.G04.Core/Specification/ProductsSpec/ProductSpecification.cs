using Store.G04.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G04.Core.Specification.ProductsSpec
{
    public class ProductSpecification : BaseSpecification<Product, int>
    {
        public ProductSpecification(int id) : base(P => P.Id == id)
        {

            ApplyIncludes();




        }
        public ProductSpecification(ProductSpecParams productSpec) : base(
           
            P =>
            (string.IsNullOrEmpty(productSpec.Search) || P.Name.ToLower().Contains(productSpec.Search))
            &&
            (!productSpec.BrandId.HasValue || productSpec.BrandId == P.BrandId)
            &&
            (!productSpec.TypeId.HasValue || productSpec.TypeId == P.TypeId)

            )
        {

            if (!string.IsNullOrEmpty(productSpec.Sort))
            {
                switch (productSpec.Sort)
                {

                    case "priceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDescinding(p => p.Price);
                        break;


                    default:
                        AddOrderBy(p => p.Name);
                        break;
                }
            }
            else
            {
                AddOrderBy(p => p.Name);
            }


            ApplyIncludes();

            ApplyPagination(productSpec.PageSize * (productSpec.PageIndex - 1), productSpec.PageSize);



        }

        private void ApplyIncludes()
        {
            Include.Add(p => p.Brand);
            Include.Add(p => p.Type);
        }
    }
}
