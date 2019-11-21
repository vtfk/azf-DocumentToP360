using System;
using System.Collections.Generic;
using System.Text;
using DocumentToP360.Models.VTFK;

namespace DocumentToP360.Models.P360.CreateDocument
{
    public class CreateDocumentRequest
    {
        public Parameter parameter { get; set; }

        //Gets input from DocToP360Request, CaseNumber from CreateCase and Recno from GetEnterprise
        public static explicit operator CreateDocumentRequest((DocToP360Request DocToP360Request, string caseNumber, int recNO) v)
        {
            return new CreateDocumentRequest()
            {
                //Request parameters for creating document
                parameter = new Parameter()
                {
                    CaseNumber = v.caseNumber,
                    Category = v.DocToP360Request.parameter.Category,
                    Title = v.DocToP360Request.parameter.Title,
                    Status = v.DocToP360Request.parameter.StatusCreateDocument,
                    URL = v.DocToP360Request.parameter.URL,
                    ResponsibleEnterpriseRecno = v.recNO,
                    StatusFile = v.DocToP360Request.parameter.StatusFile,
                    Contacts = new Contact[]{
                        new Contact()
                        {
                            ReferenceNumber = v.DocToP360Request.parameter.personalIdNumber,
                            Role = "Mottaker",
                            SearchName = v.DocToP360Request.parameter.FirstName,
                            Address = v.DocToP360Request.parameter.PrivateAddress.StreetAddress,
                            ZipCode = v.DocToP360Request.parameter.PrivateAddress.ZipCode,
                            ZipPlace = v.DocToP360Request.parameter.PrivateAddress.ZipPlace,
                            Country = v.DocToP360Request.parameter.PrivateAddress.Country
                        }

                    }

                }
            };
        }
    }

    public class Parameter
    {
        public string Title { get; set; }
        public string Category { get; set; }
        public string Status { get; set; }
        public string StatusCreateDocument { get; set; }
        public string CaseNumber { get; set; }
        public string StatusFile { get; set; }
        public List<File> Files { get; set; }
        public string URL { get; set; }
        public int ResponsibleEnterpriseRecno { get; set; }
        public Contact[] Contacts { get; set; }
    }

    public class File
    {
        public string Title { get; set; }
        public string Format { get; set; }
        public Array Data { get; set; }
        public string URL { get; internal set; }
        public string Status { get; set; }
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
