using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace UnicornSupplies
{
    public class CustomersController : ApiController
    {
        public async Task<IEnumerable<Customer>> GetCustomers()
        {
            using (var context = new UnicornSuppliesContext())
            {
                return await context.Customers
                    .AsNoTracking()
                    .ToListAsync();
            }
        }

        [ResponseType(typeof(Customer))]
        public async Task<IHttpActionResult> GetCustomer(int id)
        {
            using (var context = new UnicornSuppliesContext())
            {
                var customer = await context.Customers
                    .AsNoTracking()
                    .FirstOrDefaultAsync(o => o.Id == id);

                return customer == null
                    ? (IHttpActionResult) NotFound()
                    : Ok(customer);
            }
        }

        [HttpPost]
        [ResponseType(typeof(Customer))]
        public async Task<IHttpActionResult> InsertCustomer(Customer customer)
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

        [HttpPut]
        [ResponseType(typeof(Customer))]
        public async Task<IHttpActionResult> UpdateCustomer(Customer customer)
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
