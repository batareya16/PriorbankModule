using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PriorbankModule.Entities;

namespace PriorbankModule.Common
{
    public class ConversionHelper
    {
        public static List<Income> Convert(IEnumerable<PriorbankTransaction> list)
        {
            return list.Select(e => new Income() {
                Description = e.TransDetails,
                Place = e.TransDetails,
                DateAndTime = new DateTime(
                        e.TransDate.Year,
                        e.TransDate.Month,
                        e.TransDate.Day,
                        e.TransTime.Hour,
                        e.TransTime.Minute,
                        e.TransTime.Second),
                Summ = AmountHelper.GetAccountAmount(e.AccountAmountString)
            }).ToList();
        }

        public static List<PriorbankTransaction> Convert(IEnumerable<PriorbankLockedTransaction> list)
        {
            return list.Select(e => new PriorbankTransaction()
            {
                Amount = e.AAmount,
                TransCurr = e.ATransCurr,
                TransDate = e.ATransDate,
                TransTime = e.ATransTime,
                AccountAmount = e.ATransAmount,
                AmountString = e.AAmountString,
                TransDetails = e.ATransDetails,
                AccountAmountString = e.ATransAmountString
            }).ToList();
        }
    }
}
