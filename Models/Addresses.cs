using System;
using System.Collections.Generic;
using ContactsApi;

namespace ContactsApi
{
    public partial class Addresses
    {
        public int Id { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string CityRegion { get; set; }
        public string StateProvince { get; set; }
        public string ZipPostalCode { get; set; }
        public string Country { get; set; }
        public string Category { get; set; }
        public int ContactId { get; set; }

        public Contacts Contact { get; set; }
    }
}
