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

        public double AmountString { get; set; }

        public DateTime PostingDate { get; set; }

        public double FeeAmountString { get; set; }

        public double AccountAmountString { get; set; }

        public string ContractCurr { get; set; }

        public long Amount { get; set; }

        public long FeeAmount { get; set; }

        public long AccountAmount { get; set; }

        public string TransCurr { get; set; }

        public DateTime TransTime { get; set; }

        public bool IsHce { get; set; }
    }
}
