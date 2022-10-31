using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Newtonsoft.Json;

namespace UnicornSupplies
{
    public class OrdersController : ApiController
    {
        public async Task<IEnumerable<Order>> GetOrders()
        {
            using (var context = new UnicornSuppliesContext())
            {
                return await context.Orders
                    .Include(o => o.Customer)
                    .Include(o => o.OrderLines.Select(l => l.Product))
                    .AsNoTracking()
                    .ToListAsync();
            }
        }

        [ResponseType(typeof(Order))]
        public async Task<IHttpActionResult> GetOrder(int id)
        {
            using (var context = new UnicornSuppliesContext())
            {
                var order = await context.Orders
                    .Include(o => o.Customer)
                    .Include(o => o.OrderLines)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(o => o.Id == id);

                return order == null
                    ? (IHttpActionResult) NotFound()
                    : Ok(order);
            }
        }

        [HttpPost]
        [ResponseType(typeof(Order))]
        public async Task<IHttpActionResult> InsertOrder(Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            using (var context = new UnicornSuppliesContext())
            {
                context.Orders.Add(order);
                await context.SaveChangesAsync();
            }

            return Ok(order);
        }

        [HttpPut]
        [ResponseType(typeof(Order))]
        public async Task<IHttpActionResult> UpdateOrder(Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            using (var context = new UnicornSuppliesContext())
            {
                context.Orders.Add(order);
                context.Entry(order).State = EntityState.Modified;

                foreach (var orderLine in order.OrderLines.Where(orderLine => orderLine.Id != 0))
                {
                    context.Entry(orderLine).State = EntityState.Modified;
                }

                if (order.Customer.Id != 0)
                {
                    context.Entry(order.Customer).State = EntityState.Modified;
                }

                await context.SaveChangesAsync();
            }

            return Ok(order);
        }

        [HttpPut]
        [Route("orders/archive/{archiveDate}")]
        public async Task<IHttpActionResult> ArchiveOldOrders(DateTime archiveDate)
        {
            using (var context = new UnicornSuppliesContext())
            {
                var orders = await context.Orders
                    .Where(o => o.OrderedOn < archiveDate && !o.Archived)
                    .ToListAsync();

                foreach (var order in orders)
                {
                    order.Archived = true;
                }

                await context.SaveChangesAsync();
            }

            return Ok();
        }

        [HttpPut]
        [Route("orders/cancel/{region}")]
        public async Task<IHttpActionResult> CancelOrdersForRegion(string region)
        {
            using (var context = new UnicornSuppliesContext())
            {
                var orders = await context.Orders
                    .Where(o => o.DispatchedOn == null)
                    .Include(o => o.Customer).ToListAsync();

                foreach (var order in orders)
                {
                    var contactDetails = JsonConvert.DeserializeObject<ContactDetails>(order.Customer.ContactDetails);
                    if (contactDetails.Region == region)
                    {
                        order.Cancelled = true;
                        order.CancelledBecause
                            = "Unfortunately, we have cancelled your order because we are no longer conducting business in your region.";
                    }
                }

                await context.SaveChangesAsync();
            }

            return Ok();
        }
    }
}
