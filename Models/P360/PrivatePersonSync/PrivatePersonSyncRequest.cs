using System;
using System.Collections.Generic;
using System.Text;
using DocumentToP360.Models.VTFK;

namespace DocumentToP360.Models.P360.PrivatePersonSync
{

    public class PrivatePersonSyncRequest
    {
        public Parameter parameter { get; set; }

        //Gets input from DocToP360Request
        public static explicit operator PrivatePersonSyncRequest(DocToP360Request v)
        {
            return new PrivatePersonSyncRequest()
            {
                //Request parameters for creating a person if PersonalIdNumber doesn't exist
                parameter = new Parameter()
                {
                    FirstName = v.parameter.FirstName,
                    LastName = v.parameter.LastName,
                    PersonalIdNumber = v.parameter.personalIdNumber,
                    PrivateAddress = new Privateaddress()
                    {
                        Country = v.parameter.PrivateAddress.Country,
                        StreetAddress = v.parameter.PrivateAddress.StreetAddress,
                        ZipCode = v.parameter.PrivateAddress.ZipCode,
                        ZipPlace = v.parameter.PrivateAddress.ZipPlace

                    }

                }

            };
        }
    }

    public class Parameter
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonalIdNumber { get; set; }
        public Privateaddress PrivateAddress { get; set; }
    }

    public class Privateaddress
    {
        public string StreetAddress { get; set; }
        public string ZipCode { get; set; }
        public string ZipPlace { get; set; }
        public string Country { get; set; }
    }
}