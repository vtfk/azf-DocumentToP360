using System;
using System.Collections.Generic;
using System.Text;
using DocumentToP360.Models.VTFK;

namespace DocumentToP360.Models.P360.EnterpriseDepartmentLookup
{

    public class EnterpriseDepartmentLookupRequest
    {
        public Parameter parameter { get; set; }

        //Gets input from DocToP360Request 
        public static explicit operator EnterpriseDepartmentLookupRequest(VTFK.Parameter v)
        {
            return new EnterpriseDepartmentLookupRequest()
            {
                //Requests Initials for department
                parameter = new Parameter()
                {
                    Initials = v.Initials
                }
            };
        }
    }

    public class Parameter
    {
        public string Initials { get; set; }
    }



}
