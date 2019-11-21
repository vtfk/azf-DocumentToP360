using System;
using System.Collections.Generic;
using System.Text;
using DocumentToP360.Models.VTFK;

namespace DocumentToP360.Models.P360.UpdateCase
{
    public class UpdateCaseRequest
    {
        public Parameter parameter { get; set; }

        //Gets input from DocToP360Request and a CaseNumber from case
        public static explicit operator UpdateCaseRequest((DocToP360Request DocToP360Request, string caseNumber) v)
        {
            return new UpdateCaseRequest()
            {
                //Gets CaseNumber from CreateCase and requests input on status
                parameter = new Parameter()
                {
                    CaseNumber = v.caseNumber,
                    Status = v.DocToP360Request.parameter.StatusUpdateCase //"A"
                }
            };
        }
    }

    public class Parameter
    {
        public string CaseNumber { get; set; }
        public string Status { get; set; }
        public string StatusUpdateCase { get; set; }
    }

}
