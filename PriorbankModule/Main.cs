using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using OpenQA.Selenium.Chrome;

namespace PriorbankModule
{
    public class Main
    {
        public string GetData(ref string config)
        {
            var configObj = Serializer.Deserialize<Configuration>(config);
            var dataItems = new List<Income>();
            configObj.LastUpdate = DateTime.Now;
            config = Serializer.Serialize<Configuration>(configObj);
            return Serializer.Serialize<List<Income>>(dataItems);
        }
    }
}
