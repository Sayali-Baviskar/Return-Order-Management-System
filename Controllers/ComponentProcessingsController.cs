using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using wwe.Models;

namespace wwe.Controllers
{
    [Authorize("Admin", "User")]
    [Route("api/[controller]")]
    [ApiController]
    public class ComponentProcessingsController : ControllerBase
    {
        private readonly WweContext _context;
        public ComponentProcessingsController(WweContext context)
        {
            _context = context;
        }

        // GET: api/[controller]/Get
        [HttpGet]
        public IEnumerable<ComponentProcessing> Get()
        {
            return _context.ComponentProcessings.ToList();
        }

        [HttpPost]
        public void Post([FromBody] ComponentProcessing value)
        {
            _context.ComponentProcessings.Add(value);
            var processresponce = new ProcessResponse();
            var payment = new Payment();
            processresponce.Name = value.Name;
            value.OrderPlacedDate = DateTime.UtcNow;
            var random = new Random();
            value.RequestId = random.Next(9999, 99999);
            payment.Name = value.Name;
            payment.CreditCardNumber = value.CreditCardNumber;

            if (value.IsPriorityRequest && value.ComponentType == "Integral")
            {
                processresponce.ProcessingCharge = 700;   //prcoess charge
                processresponce.DateOfDelivery = value.OrderPlacedDate.AddDays(2);
                payment.ProcessingCharge = 700;   //prcoess charge

            }
            else 
            {
                processresponce.ProcessingCharge = 500;
                payment.ProcessingCharge = 500;
                processresponce.DateOfDelivery = value.OrderPlacedDate.AddDays(5);
            }
            if(value.ComponentType == "Accessory")
            {
                processresponce.ProcessingCharge = 300;
                payment.ProcessingCharge = 300;
                processresponce.DateOfDelivery = value.OrderPlacedDate.AddDays(5);
            }

            if (value.ComponentType == "Integral")
            {
                processresponce.PackagingAndDeliveryCharge = 300;    //packing and delivary charges
                payment.PackagingAndDeliveryCharge = 300;
            }
            else
            {
                processresponce.PackagingAndDeliveryCharge = 150;
                payment.PackagingAndDeliveryCharge = 150;
            }
            payment.PaymentStatus = false;

            processresponce.TotalCharge = value.Quantity * (processresponce.PackagingAndDeliveryCharge + processresponce.ProcessingCharge);
            payment.TotalCharge = processresponce.TotalCharge;

            _context.ComponentProcessings.Add(value);
            _context.Payments.Add(payment);
            _context.ProcessResponses.Add(processresponce);
            _context.SaveChanges();
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var order = _context.ComponentProcessings.Where(m => m.RequestId == id).FirstOrDefault();
            var order1 = _context.Payments.Where(m => m.RequestId == id).FirstOrDefault();
            var order2 = _context.ProcessResponses.Where(m => m.RequestId == id).FirstOrDefault();
            _context.ComponentProcessings.Remove(order);
            _context.ProcessResponses.Remove(order2);
            _context.Payments.Remove(order1);
            _context.SaveChanges();
        }
    }
}
