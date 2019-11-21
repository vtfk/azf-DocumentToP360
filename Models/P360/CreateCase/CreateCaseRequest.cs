using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DocumentToP360.Models.VTFK;

namespace DocumentToP360.Models.P360.CreateCase
{

    public class CreateCaseRequest
    {
        public Parameter parameter { get; set; }

        //Gets input from DocToP360Request and Recno from GetEnterprise
        public static explicit operator CreateCaseRequest((DocToP360Request DocToP360Request, int recNO) v)
        {
            return new CreateCaseRequest()
            {
                //Request parameters for creating case
                parameter = new Parameter()
                {
                    Title = v.DocToP360Request.parameter.Title,
                    AccessCode = v.DocToP360Request.parameter.AccessCode,
                    AccessGroup = v.DocToP360Request.parameter.AccessGroup,
                    ArchiveCodes = new Archivecode[] {

                        new Archivecode(){
                        ArchiveCode = v.DocToP360Request.parameter.personalIdNumber,
                        ArchiveType = "FNR",
                        Sort = 1,
                        IsManualText = true

                        },
                        new Archivecode(){
                        ArchiveCode = v.DocToP360Request.parameter.ArchiveCodes.FirstOrDefault().ArchiveCode,
                        ArchiveType = v.DocToP360Request.parameter.ArchiveCodes.FirstOrDefault().ArchiveType,
                        Sort = 2
                        }

                    },
                    Contacts = new Contact[]{
                        new Contact()
                        {
                            ReferenceNumber = v.DocToP360Request.parameter.personalIdNumber,
                            Role = "Sakspart",
                            SearchName = v.DocToP360Request.parameter.FirstName,
                            Address = v.DocToP360Request.parameter.PrivateAddress.StreetAddress,
                            ZipCode = v.DocToP360Request.parameter.PrivateAddress.ZipCode,
                            ZipPlace = v.DocToP360Request.parameter.PrivateAddress.ZipPlace,
                            Country = v.DocToP360Request.parameter.PrivateAddress.Country
                        }
                    },
                    ResponsibleEnterpriseRecno = v.recNO,
                    Status = v.DocToP360Request.parameter.StatusCreateCase, //"B"
                    SubArchive = v.DocToP360Request.parameter.SubArchive
                }
            };
        }
    }

    public class Parameter
    {
        public string Title { get; set; }
        public string AccessCode { get; set; }
        public string AccessGroup { get; set; }
        public string SubArchive { get; set; }
        public string Status { get; set; }
        public string StatusCreateCase { get; set; }
        public string Email { get; set; }
        public int ResponsibleEnterpriseRecno { get; set; }
        public Archivecode[] ArchiveCodes { get; set; }
        public Contact[] Contacts { get; set; }
    }

    public class Archivecode
    {
        public string ArchiveCode { get; set; }
        public string ArchiveType { get; set; }
        public int Sort { get; set; }
        public bool IsManualText { get; set; }
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
