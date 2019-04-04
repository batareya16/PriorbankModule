using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PriorbankModule.Services.Priorbank;

namespace PriorbankModule.Entities
{
    public class ParsedDataEntity
    {
        public List<string> SerializedData { get; set; }

        public PriorbankReceivedData ReceivedDataFlags { get; set; }
    }
}
