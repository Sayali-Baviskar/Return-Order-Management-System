using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using wwe.Models;

namespace wwe.Controllers
{
    [Authorize("Admin", "User")]
    [Route("api/[controller]")]
    [ApiController]
    public class PackageController : ControllerBase
    {
        private readonly WweContext _context;
        public PackageController(WweContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<ProcessResponse> Get()
        {
            return _context.ProcessResponses.ToList();
        }

        [HttpGet("{uname}")]
        public IEnumerable<ProcessResponse> GetByUsername(string uname)
        {
            return _context.ProcessResponses.Where(e => e.Name == uname).ToList();
        }
    }
}
