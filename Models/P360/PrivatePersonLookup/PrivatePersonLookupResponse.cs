using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentToP360.Models.P360.PrivatePersonLookup
{
    public class PrivatePersonLookupResponse
    {
        public List<Privateperson> PrivatePersons { get; set; }
        public int TotalPageCount { get; set; }
        public int TotalCount { get; set; }
        public bool Successful { get; set; }
        public object ErrorMessage { get; set; }
        public object ErrorDetails { get; set; }
    }

    public class Privateperson
    {
        public int Recno { get; set; }
        public string FirstName { get; set; }
        public object MiddleName { get; set; }
        public string LastName { get; set; }
        public string PersonalIdNumber { get; set; }
        public string ExternalID { get; set; }
        public object PhoneNumber { get; set; }
        public object MobilePhone { get; set; }
        public string Email { get; set; }
        public Privateaddress PrivateAddress { get; set; }
        public object PostAddress { get; set; }
        public Workaddress WorkAddress { get; set; }
        public object Gender { get; set; }
        public object[] Categories { get; set; }
        public string CustomNo1 { get; set; }
        public string CustomNo2 { get; set; }
        public string CustomNo3 { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string Title { get; set; }
        public string AccessGroup { get; set; }
        public object AdditionalFields { get; set; }
        public string AlternativeEmail { get; set; }
    }

    public class Privateaddress
    {
        public string StreetAddress { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string ZipPlace { get; set; }
        public string Country { get; set; }
        public string County { get; set; }
        public string Area { get; set; }
    }

    public class Workaddress
    {
        public string StreetAddress { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string ZipPlace { get; set; }
        public string Country { get; set; }
        public string County { get; set; }
        public string Area { get; set; }
    }

}
