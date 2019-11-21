using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentToP360.Models.P360.EnterpriseDepartmentLookup
{
 

    public class EnterpriseDepartmentLookupResponse
    {
        public Enterprise[] Enterprises { get; set; }
        public int TotalPageCount { get; set; }
        public int TotalCount { get; set; }
        public bool Successful { get; set; }
        public object ErrorMessage { get; set; }
        public object ErrorDetails { get; set; }
    }

    public class Enterprise
    {
        public int Recno { get; set; }
        public Contactrelation[] ContactRelations { get; set; }
        public string EnterpriseNumber { get; set; }
        public string ExternalID { get; set; }
        public string Name { get; set; }
        public object Telefax { get; set; }
        public object MobilePhone { get; set; }
        public object PhoneNumber { get; set; }
        public object Email { get; set; }
        public Postaddress PostAddress { get; set; }
        public Officeaddress OfficeAddress { get; set; }
        public string Initials { get; set; }
        public object Web { get; set; }
        public string[] Categories { get; set; }
        public string ParentEnterpriseNumber { get; set; }
        public string CustomNo1 { get; set; }
        public string CustomNo2 { get; set; }
        public string CustomNo3 { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string AccessGroup { get; set; }
        public object AdditionalFields { get; set; }
        public object AlternativeEmail { get; set; }
    }

    public class Postaddress
    {
        public string StreetAddress { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string ZipPlace { get; set; }
        public string Country { get; set; }
        public string County { get; set; }
        public string Area { get; set; }
    }

    public class Officeaddress
    {
        public string StreetAddress { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string ZipPlace { get; set; }
        public string Country { get; set; }
        public string County { get; set; }
        public string Area { get; set; }
    }

    public class Contactrelation
    {
        public string Name { get; set; }
    }

}


