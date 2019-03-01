using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PriorbankModule;
using PriorbankModule.Common;

namespace PriorbankLauncher
{
    class Program
    {
        static void Main(string[] args)
        {
            var priorbank = new Main();
            var config = Serializer.Serialize<Configuration>(
                new Configuration() {
                     CardName = "Зарплатная карта ",
                     LastUpdate = DateTime.Now.AddDays(-1),
                     Login = "shagaldemo",
                     Password = "kovalevskayademo"
                });
            var result = priorbank.GetData(ref config);
        }
    }
}