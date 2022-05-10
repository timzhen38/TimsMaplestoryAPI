#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TimsMaplestoryAPI.Models;

namespace TimsMaplestoryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        private readonly MaplestoryAPIDBContext _context;

        public ClassController(MaplestoryAPIDBContext context)
        {
            _context = context;
        }

        // GET: api/Class
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Class>>> GetClasses()
        {
            return await _context.Classes.Include(c => c.Player).ToListAsync();
        }

        // GET: api/Class/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Response>> GetClass(int id)
        {
            var @class = await _context.Classes.Include(c => c.Player).Where(c => c.ClassId == id).FirstOrDefaultAsync();

            var response = new Response();

            response.statusCode = 404;
            response.statusDescription = "CLASS NOT FOUND";

            if (@class != null)
            {
                response.statusCode = 200;
                response.statusDescription = "OK";
                response.classes.Add(@class);
            }

            return response;
        }

        // PUT: api/Class/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<Response> PutClass(int id, Class @class)
        {
            var response = new Response();

            response.statusCode = 200;
            response.statusDescription = "OK, CLASS UPDATED";

            if (id != @class.ClassId)
            {
                response.statusCode = 400;
                response.statusDescription = "BAD REQUEST";
            }

            _context.Entry(@class).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClassExists(id))
                {
                    response.statusCode = 404;
                    response.statusDescription = "CLASS NOT FOUND";
                }
                else
                {
                    throw;
                }
            }

            return response;
        }

        // POST: api/Class
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Response>> PostClass(Class @class)
        {
            var response = new Response();

            response.statusCode = 404;
            response.statusDescription = "CLASS NOT FOUND";

            if (ClassNameExists(@class.ClassName))
            {
                response.statusCode = 409;
                response.statusDescription = "CLASS ALREADY EXISTS";
                return response;
            }

            _context.Classes.Add(@class);
            await _context.SaveChangesAsync();

            if (ClassExists(@class.ClassId))
            {
                response.statusCode = 201;
                response.statusDescription = "CLASS CREATED";
            }

            //CreatedAtAction("GetClass", new { id = @class.ClassId }, @class);

            return response;
        }

        // DELETE: api/Class/5
        [HttpDelete("{id}")]
        public async Task<Response> DeleteClass(int id)
        {
            var @class = await _context.Classes.FindAsync(id);

            var response = new Response();

            response.statusCode = 200;
            response.statusDescription = "OK, CLASS DELETED";

            if (@class == null)
            {
                response.statusCode = 404;
                response.statusDescription = "CLASS NOT FOUND";
                return response;
            }

            _context.Classes.Remove(@class);
            await _context.SaveChangesAsync();

            return response;
        }

        private bool ClassExists(int id)
        {
            return _context.Classes.Any(e => e.ClassId == id);
        }

        private bool ClassNameExists(string name)
        {
            return _context.Classes.Any(e => e.ClassName == name);
        }
    }
}
