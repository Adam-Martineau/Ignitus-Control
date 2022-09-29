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
        public m32_data tank1 { get; set; }
        public m32_data tank2 { get; set; }
        public m32_data engine { get; set; }
        public power_supplie_data power { get; set; }
        public bool battery { get; set; }
        public bool armed { get; set; }
        public bool ignition { get; set; }
        public bool purge_valve { get; set; }
        public bool main_valve { get; set; }
    }

    internal class m32_data
    {
        public int add { get; set; }
        public double temp { get; set; }
        public double pressure { get; set; }
        public int status { get; set; }
    }

    internal class power_supplie_data
    {
        public int add { get; set; }
        public double power { get; set; }
        public double current { get; set; }
        public double voltage { get; set; }
    }
}
