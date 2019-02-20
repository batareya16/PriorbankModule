using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PriorbankModule.Entities
{
    public class PriorbankLockedTransaction
    {
        public DateTime ATransDate { get; set; }

        public string ATransDetails { get; set; }

        public double ATransAmountString { get; set; }

        public double AAmountString { get; set; }

        public long AAmount { get; set; }

        public long ATransAmount { get; set; }

        public string ATransCurr { get; set; }

        public DateTime ATransTime { get; set; }

        public string ContractCurr { get; set; }

        public bool IsHce { get; set; }
    }
}
