using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ContactsApi;
using SolsticeContactsApiPeterson.Models;

namespace NumbersApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class NumbersController : ControllerBase
    {
        private readonly ContactsContext _context;

        public NumbersController(ContactsContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetNumber()
        {
            return new ObjectResult(_context.Numbers);
        }
        [HttpGet("{id}", Name = "getNumber")]
        public async Task <IActionResult> GetNumber([FromRoute] int id)
        {
            var number = await _context.Numbers.SingleOrDefaultAsync(m => m.Id == id); //Single or Default avoids errors where multiple values are returned. definitely want to log if multiples are returned, but out of scope for this project
            return new ObjectResult(number);
        }
        [HttpPost]
        public async Task<IActionResult> PostNumber([FromBody] Numbers number)
        {
            _context.Numbers.Add(number);
            await _context.SaveChangesAsync();
            return CreatedAtAction("getNumber", new { id = number.Id}, number );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutNumber([FromRoute] int id, [FromBody] Object number)
        {
            _context.Entry(number).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(number);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNumber([FromRoute] int id)
        {
            var number = await _context.Numbers.SingleOrDefaultAsync(m => m.Id == id);//Single or Default avoids errors where multiple values are returned. definitely want to log if multiples are returned, but out of scope for this project
            _context.Numbers.Remove(number);
            await _context.SaveChangesAsync();
            return Ok(number); //return number for confirmation, consider Single or Default above. 
        }
    }
}