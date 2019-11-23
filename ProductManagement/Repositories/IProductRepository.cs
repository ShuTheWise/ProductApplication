using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManagement.Models.Repositories
{
    public interface IProductRepository
    {
        IEnumerable<Product> Get();

        Product Get(Guid id);

        Product Add(ProductCreateInputModel model);

        Product Update(ProductUpdateInputModel model);

        bool Delete(Guid id);
    }
}
