using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using UrbanMemory.Domain.Catelog;
using UrbanMemory.Data;

namespace UrbanMemory.Api.Controllers
{
    [ApiController]
    [Route("api/Orders")]
    public class OrdersController : ControllerBase
    {
        private readonly StoreContext _db;

        public OrdersController(StoreContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult GetOrders()
        {
            var orders = _db.Items.ToList();
            return Ok(orders);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetOrder(int id)
        {
            var order = _db.Items.Find(id);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        [HttpPost]
        public IActionResult PostOrder(Item order)
        {
            _db.Items.Add(order);
            _db.SaveChanges();
            return Created($"/api/Orders/{order.Id}", order);
        }

        [HttpPut("{id:int}")]
        public IActionResult PutOrder(int id, Item order)
        {
            if (id != order.Id)
            {
                return BadRequest();
            }

            if (_db.Items.Find(id) == null)
            {
                return NotFound();
            }

            _db.Entry(order).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _db.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteOrder(int id)
        {
            var order = _db.Items.Find(id);
            if (order == null)
            {
                return NotFound();
            }

            _db.Items.Remove(order);
            _db.SaveChanges();

            return Ok();
        }
    }
}
