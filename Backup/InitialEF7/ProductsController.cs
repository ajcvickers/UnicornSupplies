using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UnicornSupplies;

namespace UnicornSupplies
{
    [ApiController]
    public class ProductsController : ControllerBase
    {
        [HttpGet("products", Name = "GetProductsByCategory")]
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
