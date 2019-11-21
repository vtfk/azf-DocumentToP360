using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentToP360.Models.P360.UpdateCase
{


    public class UpdateCaseResponse
    {
        public int Recno { get; set; }
        public string CaseNumber { get; set; }
        public bool Successful { get; set; }
        public string ErrorMessage { get; set; }
        public object ErrorDetails { get; set; }
    }

}
