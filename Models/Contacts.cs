using SolsticeContactsApiPeterson.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContactsApi
{
    public partial class Contacts
    {
        public Contacts()
        {
            Addresses = new List<Addresses>();
            Emails = new List<Emails>();
            Numbers = new List<Numbers>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }
        public string ProfileImage { get; set; }
        public DateTime? Birthday { get; set; }
        public string Notes { get; set; }

        public List<Addresses> Addresses { get; set; }
        public List<Emails> Emails { get; set; }
        public List<Numbers> Numbers { get; set; }
    }
}
