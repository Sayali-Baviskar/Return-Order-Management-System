using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using wwe.Models;

namespace wwe.Controllers
{
    [Authorize("Admin", "User")]
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly WweContext _context;
        public PaymentsController(WweContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Payment> Get()
        {
            return _context.Payments.ToList();
        }

        [HttpGet("{uname}")]
        public IEnumerable<Payment> GetByUsername(string uname)
        {
            return _context.Payments.Where(e => e.Name == uname).ToList();
        }

        [HttpPut]
        [Route("/Edit/{id}")]
        public string Put(int id, [FromBody] Payment value)
        {
            if (RequestIdExists(id))
            {
                var payment = _context.Payments.Where(m => m.RequestId == id).First();
                if (payment.PaymentStatus == false)
                {
                    payment.PaymentStatus = true;
                }
                else
                {
                    payment.PaymentStatus = false;
                }

                _context.SaveChanges();
                var sucess = "Sucessfully updated";
                return sucess;
            }

            else
            {
                var error = "Invalid request Id";
                return error;
            }

        }
        private bool RequestIdExists(int id)
        {
            return _context.Payments.Any(e => e.RequestId == id);
        }
    }
}
