using DocumentToP360.Models.P360.CreateDocument;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentToP360.Models.VTFK
{
    //Input parameters for the service
    public class DocToP360Request
    {
        public Parameter parameter { get; set; }
    }

    public class Parameter
    {
        public string personalIdNumber { get; set; }
        public string Title { get; set; }
        public string AccessCode { get; set; }
        public string AccessGroup { get; set; }
        public string SubArchive { get; set; }
        public string Status { get; set; }
        public string StatusCreateDocument { get; set; }
        public string StatusUpdateCase { get; set; }
        public string StatusCreateCase { get; set; }
        public string StatusFile { get; set; }
        public string StatusCode { get; set; }
        public int ResponsibleEnterpriseRecno { get; set; }
        public List<Archivecode> ArchiveCodes { get; set; }
        public string CaseNumber { get; set; }
        public string Category { get; set; }
        public List<File> Files { get; set; }
        public string Initials { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string URL { get; set; }
        public string Email { get; set; }
        public Privateaddress PrivateAddress { get; set; }
        public Contact[] Contacts { get; set; }
    }

    public class Privateaddress
    {
        public string StreetAddress { get; set; }
        public string ZipCode { get; set; }
        public string ZipPlace { get; set; }
        public string Country { get; set; }
    }
    public class Archivecode
    {
        public string ArchiveCode { get; set; }
        public string ArchiveType { get; set; }
        public int Sort { get; set; }
        public bool IsManualText { get; set; }
    }
    public class File
    {
        public string Title { get; set; }
        public string Format { get; set; }
        public string Status { get; set; }
        public string StatusFile { get; set; }
        public Array Data { get; set; }
    }
    public class Contact
    {
        public string ReferenceNumber { get; set; }
        public object ExternalId { get; set; }
        public string Role { get; set; }
        public string SearchName { get; set; }
        public string ContactRecno { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public string ZipPlace { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
    }
}
