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
        private readonly UnicornSuppliesContext _context;

        public CustomersController(UnicornSuppliesContext context)
        {
            _context = context;
        }

        [HttpGet("customers", Name = "GetCustomers")]
        public async Task<IEnumerable<Customer>> GetCustomers() 
            => await _context.Customers
                .AsNoTracking()
                .ToListAsync();

        [HttpGet("customers/{id}", Name = "GetCustomer")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            var customer = await _context.Customers
                .AsNoTracking()
                .FirstOrDefaultAsync(o => o.Id == id);

            return customer == null
                ? NotFound()
                : Ok(customer);
        }

        [HttpPost("customers", Name = "InsertCustomer")]
        public async Task<ActionResult<Customer>> InsertCustomer(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return Ok(customer);
        }

        [HttpPut("customers", Name = "UpdateCustomer")]
        public async Task<ActionResult<Customer>> UpdateCustomer(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Entry(customer).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(customer);
        }
    }
}
