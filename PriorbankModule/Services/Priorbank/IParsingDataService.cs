using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PriorbankModule.Services.Priorbank
{
    interface IParsingDataService
    {
        List<string> ParseCardData();
    }
}
