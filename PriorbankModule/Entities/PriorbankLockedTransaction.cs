using System;
using System.Collections;

namespace PriorbankModule.Entities
{
    public class PriorbankLockedTransaction : IEqualityComparer
    {
        public DateTime ATransDate { get; set; }

        public string ATransDetails { get; set; }

        public string ATransAmountString { get; set; }

        public string AAmountString { get; set; }

        public long AAmount { get; set; }

        public long ATransAmount { get; set; }

        public string ATransCurr { get; set; }

        public DateTime ATransTime { get; set; }

        public string ContractCurr { get; set; }

        public bool IsHce { get; set; }

        public new bool Equals(object x, object y)
        {
            var trans1 = (PriorbankLockedTransaction)x;
            var trans2 = (PriorbankLockedTransaction)y;
            return trans1.ATransDate == trans2.ATransDate
                   && trans1.ATransTime == trans2.ATransTime
                   && trans1.ATransDetails == trans2.ATransDetails
                   && trans1.ATransAmountString == trans2.ATransAmountString;
        }

        public int GetHashCode(object obj)
        {
            return base.GetHashCode();
        }
    }
}
