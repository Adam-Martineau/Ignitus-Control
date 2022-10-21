using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Ignitus_Control
{
    internal class system_data
    {
        public int tank1_add { get; set; }
        public double tank1_temp { get; set; }
        public int tank1_status { get; set; }
        public double tank1_pressure { get; set; }
        public int tank2_add { get; set; }
        public double tank2_temp { get; set; }
        public int tank2_status { get; set; }
        public double tank2_pressure { get; set; }
        public int power_add { get; set; }
        public double power_power { get; set; }
        public double power_current { get; set; }
        public double power_voltage { get; set; }
        public bool battery { get; set; }
        public bool armed { get; set; }
        public bool ignition { get; set; }
        public bool purge_valve { get; set; }
        public bool main_valve { get; set; }
    }
}
