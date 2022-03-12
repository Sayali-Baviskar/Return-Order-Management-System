using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wwe.Models;

namespace wwe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly WweContext _context;

        public UsersController(WweContext context)
        {
            _context = context;
        }

        [Authorize("Admin")]
        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        [Authorize("Admin")]
        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [Authorize("Admin", "User")]
        // PUT: api/Users/5        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.UserID)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExistsId(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [AllowAnonymous]
        // POST: api/Users        
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            if (!UserExists(user.UserName))
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return Content("Success");
            }
            return Content("User already exists");
        }

        [Authorize("Admin")]
        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(string uname)
        {
            return _context.Users.Any(e => e.UserName == uname);
        }
        private bool UserExistsId(int id)
        {
            return _context.Users.Any(e => e.UserID == id);
        }
    }
}
