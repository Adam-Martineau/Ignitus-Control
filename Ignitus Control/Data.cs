using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ignitus_Control
{
    internal class Data
    {
        public double tankPressure { get; set; }
        public double tankTemperature { get; set; }
        public double enginePressure { get; set; }
        public double engineTemperature { get; set; }
        public bool isBattery { get; set; }
        public bool isRocketArmed { get; set; }
        public bool isIgnited { get; set; }
    }
}
