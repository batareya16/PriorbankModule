using System;
using System.Collections;

namespace PriorbankModule.Entities
{
    public class PriorbankTransaction : IEqualityComparer
    {
        public DateTime TransDate { get; set; }

        public string TransDetails { get; set; }

        public string AmountString { get; set; }

        public DateTime PostingDate { get; set; }

        public string FeeAmountString { get; set; }

        public string AccountAmountString { get; set; }

        public string ContractCurr { get; set; }

        public long Amount { get; set; }

        public long FeeAmount { get; set; }

        public long AccountAmount { get; set; }

        public string TransCurr { get; set; }

        public DateTime TransTime { get; set; }

        public bool IsHce { get; set; }

        public new bool Equals(object x, object y)
        {
            var trans1 = (PriorbankTransaction)x;
            var trans2 = (PriorbankTransaction)y;
            return trans1.TransDate == trans2.TransDate
                && trans1.TransTime == trans2.TransTime
                && trans1.PostingDate == trans2.PostingDate
                && trans1.TransDetails == trans2.TransDetails
                && trans1.AmountString == trans2.AmountString;
        }

        public int GetHashCode(object obj)
        {
            return base.GetHashCode();
        }
    }
}
