using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PriorbankModule.Services.Priorbank
{
    [Flags]
    public enum PriorbankReceivedData
    {
        None = 0,
        LockedOperations = 1,
        ContractOperations = 2,
        CardOperations = 4
    }
}
