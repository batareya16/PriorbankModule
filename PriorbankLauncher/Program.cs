using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PriorbankModule;

namespace PriorbankLauncher
{
    class Program
    {
        static void Main(string[] args)
        {
            var priorbank = new Main();
            var config = Configuration.GetConfiguration();
            var result = priorbank.GetData(ref config);
        }
    }
}
