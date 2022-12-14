using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace UnicornSupplies
{
    [ApiController]
    public class OrdersController : ControllerBase
    {
        [HttpGet("orders", Name = "GetOrders")]
        public async Task<IEnumerable<Order>> GetOrders()
        {
            using (var context = new UnicornSuppliesContext())
            {
                return await context.Orders
                    .Include(o => o.Customer)
                    .Include(o => o.OrderLines).ThenInclude(o => o.Product)
                    .AsNoTracking()
                    .ToListAsync();
            }
        }

        [HttpGet("orders/{id}", Name = "GetOrder")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            using (var context = new UnicornSuppliesContext())
            {
                var order = await context.Orders
                    .Include(o => o.Customer)
                    .Include(o => o.OrderLines)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(o => o.Id == id);
        
                return order == null
                    ? NotFound()
                    : Ok(order);
            }
        }
        
        [HttpPost("orders", Name = "InsertOrder")]
        public async Task<ActionResult<Order>> InsertOrder(Order order)
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
        
        [HttpPut("orders", Name = "UpdateOrder")]
        public async Task<ActionResult<Order>> UpdateOrder(Order order)
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
        
        [HttpPut("orders/archive/{archiveDate}", Name = "ArchiveOldOrders")]
        public async Task<ActionResult> ArchiveOldOrders(DateTime archiveDate)
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
        
        [HttpPut("orders/cancel/{region}", Name = "CancelOrdersForRegion")]
        public async Task<ActionResult> CancelOrdersForRegion(string region)
        {
            using (var context = new UnicornSuppliesContext())
            {
                var orders = await context.Orders
                    .Where(o => o.DispatchedOn == null)
                    .Include(o => o.Customer).ToListAsync();
        
                foreach (var order in orders)
                {
                    var contactDetails = JsonConvert.DeserializeObject<ContactDetails>(order.Customer.ContactDetails)!;
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
