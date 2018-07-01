using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ContactsApi;
using SolsticeContactsApiPeterson.Models;

namespace AddressesApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private readonly ContactsContext _context;
        public AddressesController(ContactsContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetAddress()
        {
            return new ObjectResult(_context.Addresses);
        }
        [HttpGet("{id}", Name = "getAddress")]
        public async Task<IActionResult> GetAddress([FromRoute] int id)
        {
            var address = await _context.Addresses.SingleOrDefaultAsync(m => m.Id == id); //Single or Default avoids errors where multiple values are returned. definitely want to log if multiples are returned, but out of scope for this project
            return new ObjectResult(address);
        }
        [HttpPost]
        public async Task<IActionResult> PostAddress([FromBody] Addresses address)
        {
            _context.Addresses.Add(address);
            await _context.SaveChangesAsync();
            return CreatedAtAction("getAddress", new { id = address.Id }, address);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAddress([FromRoute] int id, [FromBody] Addresses address)
        {
            _context.Entry(address).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(address);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddress([FromRoute] int id)
        {
            var address = await _context.Addresses.SingleOrDefaultAsync(m => m.Id == id);//Single or Default avoids errors where multiple values are returned. definitely want to log if multiples are returned, but out of scope for this project
            _context.Addresses.Remove(address);
            await _context.SaveChangesAsync();
            return Ok(address); //return number for confirmation, consider Single or Default above. 
        }
    }
}