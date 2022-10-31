using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using UnicornSupplies;

namespace UnicornSupplies
{
    public class ProductsController : ApiController
    {
        public async Task<IEnumerable<Category>> GetProductsByCategory()
        {
            using (var context = new UnicornSuppliesContext())
            {
                return await context.Categories
                    .Include(category => category.Products)
                    .AsNoTracking()
                    .ToListAsync();
            }
        }
    }
}
