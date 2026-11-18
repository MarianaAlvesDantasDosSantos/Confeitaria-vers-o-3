using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using confeitaria_api.Data;
using confeitaria_api.Models;

namespace confeitaria_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItensPedidoController : ControllerBase
    {
        private readonly ConfeitariaContext _context;

        public ItensPedidoController(ConfeitariaContext context)
        {
            _context = context;
        }

        // GET: api/ItensPedido
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemPedido>>> GetItensPedido()
        {
            return await _context.ItensPedido.ToListAsync();
        }

        // GET: api/ItensPedido/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemPedido>> GetItemPedido(int id)
        {
            var itemPedido = await _context.ItensPedido.FindAsync(id);

            if (itemPedido == null)
            {
                return NotFound();
            }

            return itemPedido;
        }

        // PUT: api/ItensPedido/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItemPedido(int id, ItemPedido itemPedido)
        {
            if (id != itemPedido.PedidoId)
            {
                return BadRequest();
            }

            _context.Entry(itemPedido).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemPedidoExists(id))
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

        // POST: api/ItensPedido
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ItemPedido>> PostItemPedido(ItemPedido itemPedido)
        {
            _context.ItensPedido.Add(itemPedido);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ItemPedidoExists(itemPedido.PedidoId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetItemPedido", new { id = itemPedido.PedidoId }, itemPedido);
        }

        // DELETE: api/ItensPedido/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItemPedido(int id)
        {
            var itemPedido = await _context.ItensPedido.FindAsync(id);
            if (itemPedido == null)
            {
                return NotFound();
            }

            _context.ItensPedido.Remove(itemPedido);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ItemPedidoExists(int id)
        {
            return _context.ItensPedido.Any(e => e.PedidoId == id);
        }
    }
}
