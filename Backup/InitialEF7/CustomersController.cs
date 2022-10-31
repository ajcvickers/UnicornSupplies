using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace UnicornSupplies
{
    [ApiController]
    public class CustomersController : ControllerBase
    {
        [HttpGet("customers", Name = "GetCustomers")]
        public async Task<IEnumerable<Customer>> GetCustomers()
        {
            using (var context = new UnicornSuppliesContext())
            {
                return await context.Customers
                    .AsNoTracking()
                    .ToListAsync();
            }
        }

        [HttpGet("customers/{id}", Name = "GetCustomer")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            using (var context = new UnicornSuppliesContext())
            {
                var customer = await context.Customers
                    .AsNoTracking()
                    .FirstOrDefaultAsync(o => o.Id == id);
        
                return customer == null
                    ? NotFound()
                    : Ok(customer);
            }
        }
        
        [HttpPost("customers", Name = "InsertCustomer")]
        public async Task<ActionResult<Customer>> InsertCustomer(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
        
            using (var context = new UnicornSuppliesContext())
            {
                context.Customers.Add(customer);
                await context.SaveChangesAsync();
            }
        
            return Ok(customer);
        }
        
        [HttpPut("customers", Name = "UpdateCustomer")]
        public async Task<ActionResult<Customer>> UpdateCustomer(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
        
            using (var context = new UnicornSuppliesContext())
            { 
                context.Entry(customer).State = EntityState.Modified;
                await context.SaveChangesAsync();
            }
        
            return Ok(customer);
        }
    }
}
