using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PriorbankModule.Entities
{
    public class PriorbankTransaction
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
    }
}
