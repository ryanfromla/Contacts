using Microsoft.EntityFrameworkCore;
using ContactsApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SolsticeContactsApiPeterson.Models;

namespace ContactsApi.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private ContactsContext _context;

        public ContactRepository(ContactsContext context)
        {
            _context = context;
        }

        public async Task<Contacts> Add(Contacts contact)
        {
            await _context.Contacts.AddAsync(contact);
            await _context.SaveChangesAsync();
            return contact;
        }

        public async Task<bool> Exists(int id)
        {
            return await _context.Contacts.Include(contact => contact.Addresses).Include(contact => contact.Emails).Include(contact => contact.Numbers).AnyAsync(c => c.Id == id);
        }

        public async Task<Contacts> Find(int id)
        {
            return await _context.Contacts.Include(contact => contact.Addresses).Include(contact=>contact.Emails).Include(contact=>contact.Numbers).SingleOrDefaultAsync(m=>m.Id == id);
        }

        public IEnumerable<Contacts> GetAll()
        {
            return _context.Contacts;
        }

        public async Task<Contacts> RemoveAsync(int id)
        {
            var contact = await _context.Contacts.Include(c => c.Addresses).Include(c => c.Emails).Include(c => c.Numbers).SingleAsync(a => a.Id == id);
            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();
            return contact;
        }

        public async Task<Contacts> Update(Contacts contact)
        {
            _context.Contacts.Update(contact);
            await _context.SaveChangesAsync();
            return contact;
        }
    }
}
