using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace PriorbankModule
{
    public static class Main
    {
        public static IEnumerable GetData()
        {
            var dataItems = new List<BaseModel>();
            dataItems.Add(new Income() { DateAndTime = DateTime.Now, Description = "Test Income", Summ = 100 });
            dataItems.Add(new Spending() { DateAndTime = DateTime.Now, Description = "Test Spending", Summ = 100, Place = "Place" });
            return dataItems;
        }
    }
}
