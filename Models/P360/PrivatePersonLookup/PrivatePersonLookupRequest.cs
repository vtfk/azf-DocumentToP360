using System;
using System.Collections.Generic;
using System.Text;
using DocumentToP360.Models.VTFK;

namespace DocumentToP360.Models.P360.PrivatePersonLookup
{
    public class PrivatePersonLookupRequest
    {
        public Parameter parameter { get; set; }

        //Gets PersonalIdNumber from DocToP360Request 
        public static explicit operator PrivatePersonLookupRequest(DocToP360Request docToP360Request)
        {
            return new PrivatePersonLookupRequest()
            {
                //Request parameters for checking if a person exists in P360
                parameter = new Parameter()
                {
                    PersonalIdNumber = docToP360Request.parameter.personalIdNumber
                }
            };
        }
    }

    public class Parameter
    {
        public string PersonalIdNumber { get; set; }
    }
}
