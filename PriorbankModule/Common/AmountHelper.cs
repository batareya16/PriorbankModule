using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PriorbankModule.Common
{
    public static class AmountHelper
    {
        internal static double InitialBudgetAmount { get; set; }

        public static string ReverseAccountAmountString(string accountAmountString)
        {
            return (-GetAccountAmount(accountAmountString)).ToString();
        }

        public static double GetAccountAmount(string accountAmountString)
        {
            return Convert.ToDouble(accountAmountString.Replace("  ", string.Empty).Replace('.', ','));
        }
    }
}
