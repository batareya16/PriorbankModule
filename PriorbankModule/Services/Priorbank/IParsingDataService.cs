using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PriorbankModule.Entities;

namespace PriorbankModule.Services.Priorbank
{
    interface IParsingDataService
    {
        ParsedDataEntity ParseCardData();
    }
}
