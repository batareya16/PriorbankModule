using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PriorbankModule
{
    public abstract class BaseModel
    {
        public string Description { get; set; }

        public double Summ { get; set; }

        public DateTime DateAndTime { get; set; }
    }
}
