using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat
{
    public class ProductWithTypeAndBrandSpecification:BaseSpecification<Product>
    {
        //this constructor to get all products
        public ProductWithTypeAndBrandSpecification(ProductSpecParam ProductSpec)
            :base(p=>
            (string.IsNullOrEmpty(ProductSpec.Search) || p.Name.ToLower().Contains(ProductSpec.Search))&& //Search by Product Name
            (!ProductSpec.TypeId.HasValue || p.ProductTypeId==ProductSpec.TypeId.Value) && //for filteration
            (!ProductSpec.BrandId.HasValue || p.ProductBrandId==ProductSpec.BrandId.Value)    //not hasvalue=>true
                 )
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);
            AddOrderBy(p => p.Name); //we set this line to set value if sort is null

            //pageIndex=2,pageSize=5
            ApplyPagination(ProductSpec.PageSize * (ProductSpec.PageIndex - 1), ProductSpec.PageSize);

            #region Sorting
            if (!string.IsNullOrEmpty(ProductSpec.Sort))
            {
                switch (ProductSpec.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(p => p.Price); break;
                    case "priceDesc":
                        AddOrderByDescending(p => p.Price); break;
                    default:
                        AddOrderBy(p => p.Name); break;
                }
            }
            #endregion
        }
    }
}
