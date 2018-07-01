using ContactsApi;
using System;
using System.Collections.Generic;

namespace SolsticeContactsApiPeterson.Models
{
    public partial class Numbers
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public string PhoneNumber { get; set; }
        public int ContactId { get; set; }

        public Contacts Contact { get; set; }
    }
}
