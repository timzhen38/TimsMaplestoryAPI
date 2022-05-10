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
    public class PlayerController : ControllerBase
    {
        private readonly MaplestoryAPIDBContext _context;

        public PlayerController(MaplestoryAPIDBContext context)
        {
            _context = context;
        }

        // GET: api/Player
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Player>>> GetPlayers()
        {
            return await _context.Players.ToListAsync();
        }

        // GET: api/Player/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Response>> GetPlayer(int id)
        {
            var player = await _context.Players.FindAsync(id);

            var response = new Response();

            response.statusCode = 404;
            response.statusDescription = "PLAYER NOT FOUND";

            if (player != null)
            {
                response.statusCode = 200;
                response.statusDescription = "OK";
                response.players.Add(player);
            }

            return response;
        }

        // PUT: api/Player/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<Response> PutPlayer(int id, Player player)
        {
            var response = new Response();

            response.statusCode = 200;
            response.statusDescription = "OK, PLAYER UPDATED";

            if (id != player.PlayerId)
            {
                response.statusCode = 400;
                response.statusDescription = "BAD REQUEST";
            }

            _context.Entry(player).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlayerExists(id))
                {
                    response.statusCode = 404;
                    response.statusDescription = "PLAYER NOT FOUND";
                }
                else
                {
                    throw;
                }
            }

            return response;
        }

        // POST: api/Player
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Response>> PostPlayer(Player player)
        {
            var response = new Response();

            response.statusCode = 404;
            response.statusDescription = "PLAYER NOT FOUND";

            if (PlayerNameExists(player.PlayerName))
            {
                response.statusCode = 409;
                response.statusDescription = "PLAYER ALREADY EXISTS";
                return response;
            }

            _context.Players.Add(player);
            await _context.SaveChangesAsync();

            if (PlayerExists(player.PlayerId))
            {
                response.statusCode = 201;
                response.statusDescription = "PLAYER CREATED";
            }

            //return CreatedAtAction("GetPlayer", new { id = player.PlayerId }, player);
            return response;
        }

        // DELETE: api/Player/5
        [HttpDelete("{id}")]
        public async Task<Response> DeletePlayer(int id)
        {
            var response = new Response();

            response.statusCode = 200;
            response.statusDescription = "OK, PLAYER DELETED";

            var player = await _context.Players.FindAsync(id);
            if (player == null)
            {
                response.statusCode = 404;
                response.statusDescription = "PLAYER NOT FOUND";
                return response;
            }

            _context.Players.Remove(player);
            await _context.SaveChangesAsync();

            return response;
        }

        private bool PlayerExists(int id)
        {
            return _context.Players.Any(e => e.PlayerId == id);
        }

        private bool PlayerNameExists(string name)
        {
            return _context.Players.Any(e => e.PlayerName == name);
        }
    }
}
