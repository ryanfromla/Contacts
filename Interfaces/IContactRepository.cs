using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactsApi.Interfaces
{
    public interface IContactRepository
    {
        Task<Contacts> Add(Contacts contact);
        IEnumerable<Contacts> GetAll();
        Task<Contacts> Find(int id);
        Task<Contacts> Update(Contacts contact);
        Task<Contacts> RemoveAsync(int id);
        Task<bool> Exists(int id);
    }
}
