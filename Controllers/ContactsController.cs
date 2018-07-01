using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ContactsApi;
using Microsoft.EntityFrameworkCore.Query;
using SolsticeContactsApiPeterson.Models;

namespace ContactsApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly ContactsContext _context;

        public ContactsController(ContactsContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetContact()
        {
            var joinedContacts = _context.Contacts
                .Include(a => a.Emails)
                .Include(a => a.Addresses)
                .Include(a => a.Numbers);
            //add Count of results for validation
            if (joinedContacts.Count() > 0)
                return new ObjectResult(joinedContacts) { StatusCode = 200 };
            else
                return new ObjectResult(null) { StatusCode = 404 };
        }
        [HttpGet("{id}", Name = "getContact")]
        public async Task<IActionResult> GetContact([FromRoute] int id)
        {
            //add check if Customer Exists
            var Contact = await _context.Contacts
                .Include(a => a.Emails)
                .Include(a => a.Addresses)
                .Include(a => a.Numbers)
                .SingleOrDefaultAsync(m => m.Id == id);//.Include(a=>a.Addresses).Include(a=>a.Numbers).SingleOrDefaultAsync(m => m.Id == id);//Single or Default avoids errors where multiple values are returned. definitely want to log if multiples are returned, but out of scope for this project
            return new ObjectResult(Contact);
        }
        [HttpPost]
        public async Task<IActionResult> PostContact([FromBody] Contacts Contact)
        {
            //add validation for modelstate
            _context.Contacts.Add(Contact);
            await _context.SaveChangesAsync();
            return CreatedAtAction("getContact", new { id = Contact.Id }, Contact);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutContact([FromRoute] int id, [FromBody] Object Contact)
        {
            _context.Entry(Contact).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(Contact);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact([FromRoute] int id)
        {
            var Contact = await _context.Contacts.SingleOrDefaultAsync(m => m.Id == id);//Single or Default avoids errors where multiple values are returned. definitely want to log if multiples are returned, but out of scope for this project
            _context.Contacts.Remove(Contact);
            await _context.SaveChangesAsync();
            return Ok(Contact); //return Contact for confirmation, consider Single or Default above. 
        }
    }
}