using System.Collections.Generic;
using PriorbankModule.Entities;

namespace PriorbankModule.Services.Priorbank
{
    interface IIncomeProcessor
    {
        List<Income> ProcessIncomes();
    }
}
