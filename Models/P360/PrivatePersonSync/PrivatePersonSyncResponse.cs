using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentToP360.Models.P360.PrivatePersonSync
{

    public class PrivatePersonSyncResponse
    {
        public int Recno { get; set; }
        public bool Successful { get; set; }
        public object ErrorMessage { get; set; }
        public object ErrorDetails { get; set; }
    }

}
