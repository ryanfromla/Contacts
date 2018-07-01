using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ContactsApi;
using SolsticeContactsApiPeterson.Models;

namespace EmailsApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class EmailsController : ControllerBase
    {
        private readonly ContactsContext _context;

        public EmailsController(ContactsContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetEmail()
        {
            return new ObjectResult(_context.Emails);
        }
        [HttpGet("{id}", Name = "getEmail")]
        public async Task<IActionResult> GetEmail([FromRoute] int id)
        {
            var Email = await _context.Emails.SingleOrDefaultAsync(m => m.Id == id); //Single or Default avoids errors where multiple values are returned. definitely want to log if multiples are returned, but out of scope for this project
            return new ObjectResult(Email);
        }
        [HttpPost]
        public async Task<IActionResult> PostEmail([FromBody] Emails Email)
        {
            _context.Emails.Add(Email);
            await _context.SaveChangesAsync();
            return CreatedAtAction("getEmail", new { id = Email.Id }, Email);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmail([FromRoute] int id, [FromBody] Object Email)
        {
            _context.Entry(Email).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(Email);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmail([FromRoute] int id)
        {
            var Email = await _context.Emails.SingleOrDefaultAsync(m => m.Id == id);//Single or Default avoids errors where multiple values are returned. definitely want to log if multiples are returned, but out of scope for this project
            _context.Emails.Remove(Email);
            await _context.SaveChangesAsync();
            return Ok(Email); //return Email for confirmation, consider Single or Default above. 
        }
    }
}